using Microsoft.AspNetCore.Identity;
using TMAWarehouse.Web.Models.Dto;

namespace TMAWarehouse.Web.Services.IServices
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDto);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDto);
        Task<ResponseDto?> GetUsers();
        Task<ResponseDto?> GetUser(string userId);
		Task<ResponseDto?> SetRole(SetRoleRequestDto data);
        Task<ResponseDto?> GetRole(string userId);
	}
}
