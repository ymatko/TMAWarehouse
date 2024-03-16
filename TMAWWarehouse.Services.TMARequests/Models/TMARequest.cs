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

        [Required]
        public int ItemID { get; set; }
        [NotMapped]
        public ItemDto? Item { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
