using Microsoft.AspNetCore.Identity;
using TMAWarehouse.Service.Auth.Models.Dto;

namespace TMAWarehouse.Service.Auth.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> AssignRole(string email, string roleName);
        Task<IEnumerable<IdentityUser>> GetUsers();
    }
}
