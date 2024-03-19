using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TMAWarehouse.Service.Auth.Models.Dto;
using TMAWarehouse.Service.Auth.Service.IService;
using TMAWarehouse.Services.Auth.Models.Dto;

namespace TMAWarehouse.Service.Auth.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private ResponseDto _response;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
            _response = new ResponseDto();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto model)
        {
            var errorMessage = await _authService.Register(model);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Username or password is incorrect";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Error encountered";
                return BadRequest(_response);
            }
            return Ok(_response);
        }
        
        [HttpGet("GetAllUsers")]
        public async Task<ResponseDto?> GetAllUsers()
        {
            try
            {
                IEnumerable<IdentityUser> users = await _authService.GetUsers();
                _response.Result = users;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("GetUser/{id}")]
        public async Task<ResponseDto?> GetUser(string id)
		{
			try
			{
				IdentityUser user = await _authService.GetUser(id);
				_response.Result = user;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}

		[HttpPost("SetRole")]
        public async Task<ResponseDto?> SetRole([FromBody] SetRoleRequestDto request)
		{
			try
			{
				IdentityUser newUser = await _authService.ChangeRole(request.User.Email, request.RoleName);
				_response.Result = newUser;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
        [HttpGet("GetRole")]
        public async Task<ResponseDto?> GetRole([FromBody] string userId)
        {
			try
			{
				string result = await _authService.GetRole(userId);
				_response.Result = result;
			}
			catch (Exception ex)
			{
				_response.IsSuccess = false;
				_response.Message = ex.Message;
			}
			return _response;
		}
	}
}
