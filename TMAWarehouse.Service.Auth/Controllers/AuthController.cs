﻿using Microsoft.AspNetCore.Http;
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
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
