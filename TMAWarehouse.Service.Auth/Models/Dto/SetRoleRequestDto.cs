using Microsoft.AspNetCore.Identity;

namespace TMAWarehouse.Service.Auth.Models.Dto
{
	public class SetRoleRequestDto
	{
		public IdentityUser User { get; set; }
		public string RoleName { get; set; }
	}
}
