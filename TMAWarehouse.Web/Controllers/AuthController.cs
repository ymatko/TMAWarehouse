using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;
using System.IdentityModel.Tokens.Jwt;
using TMAWarehouse.Web.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http.Metadata;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace TMAWarehouse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;

        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginDto)
        {
            ResponseDto? result = await _authService.LoginAsync(loginDto);

            if (result != null && result.IsSuccess)
            {
                LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(result.Result));

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["error"] = result.Message;
                return View(loginDto);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCoordinator, Value=SD.RoleCoordinator},
                new SelectListItem{Text=SD.RoleEmployee, Value=SD.RoleEmployee}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto registrationDto)
        {
            ResponseDto? result = await _authService.RegisterAsync(registrationDto);
            ResponseDto? assignRole;

            if(result != null && result.IsSuccess)
            {
                if (string.IsNullOrEmpty(registrationDto.Role))
                {
                    registrationDto.Role = SD.RoleEmployee;
                }
                assignRole = await _authService.AssignRoleAsync(registrationDto);
                if (assignRole != null && assignRole.IsSuccess) 
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            else
            {
                TempData["error"] = result.Message;
            }

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCoordinator, Value=SD.RoleCoordinator},
                new SelectListItem{Text=SD.RoleEmployee, Value=SD.RoleEmployee}
            };
            ViewBag.RoleList = roleList;
            return View(registrationDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value));
            identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value));

            identity.AddClaim(new Claim(ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value));
            identity.AddClaim(new Claim(ClaimTypes.Role,
                jwt.Claims.FirstOrDefault(u => u.Type == "role").Value));

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }

		public IActionResult UserIndex()
		{
			return View();
		}

		[HttpGet]
        public IActionResult GetAll()
        {
            List<IdentityUser> list;
            ResponseDto response = _authService.GetUsers().GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<IdentityUser>>(Convert.ToString(response.Result));
            }
            else
            {
                list = new List<IdentityUser>();
            }
            return Json(new { data = list });
        }
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<string> GetRole(string userId)
		{
			ResponseDto? result = await _authService.GetRole(userId);

			if (result != null && result.IsSuccess)
			{
				return result.Result.ToString();
			}
			else
			{
				return result?.Message ?? "An error occurred.";
			}
		}
        [HttpGet]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ChangeRole(string userId, string newRole)
        {
            var result = await _authService.GetUser(userId);
            IdentityUser? user;

            if (result != null && result.IsSuccess)
            {
                user = JsonConvert.DeserializeObject<IdentityUser>(Convert.ToString(result.Result));
                await _authService.SetRole(new SetRoleRequestDto { User = user, RoleName = newRole });
                TempData["success"] = "User role has been successfully changed";
            }
            else
            {
                TempData["error"] = "Failed to change user role.";
            }
            return RedirectToAction("UserIndex");
        }
    }
}
