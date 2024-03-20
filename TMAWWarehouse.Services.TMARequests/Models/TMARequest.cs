using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMAWarehouse.Services.TMARequest.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests.Models
{
    public class TMARequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        public string? Status { get; set; }
    }
}
