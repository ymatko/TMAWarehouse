using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Runtime.CompilerServices;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;

namespace TMAWarehouse.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new();
            return View(loginRequestDto);
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

            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCoordinator, Value=SD.RoleCoordinator},
                new SelectListItem{Text=SD.RoleEmployee, Value=SD.RoleEmployee}
            };
            ViewBag.RoleList = roleList;
            return View(registrationDto);
        }

        public IActionResult Logout()
        {
            return View();
        }
    }
}
