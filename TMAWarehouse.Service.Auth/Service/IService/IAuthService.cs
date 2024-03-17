using TMAWarehouse.Service.Auth.Models.Dto;

namespace TMAWarehouse.Service.Auth.Service.IService
{
    public interface IAuthService
    {
        Task<string> Register(RegistrationRequestDto registrationRequestDto);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
    }
}
