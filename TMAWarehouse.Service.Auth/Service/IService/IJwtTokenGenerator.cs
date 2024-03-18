using Microsoft.AspNetCore.Identity;

namespace TMAWarehouse.Service.Auth.Service.IService
{
    public interface IJwtTokenGenerator
    {
        string GenerateToken(IdentityUser user, IEnumerable<string> roles);
    }
}
