using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.Web.Models.Dto
{
    public class LoginRequestDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
