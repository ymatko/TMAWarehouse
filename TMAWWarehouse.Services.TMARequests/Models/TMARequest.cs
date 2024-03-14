using System.ComponentModel.DataAnnotations;

namespace TMAWWarehouse.Services.TMARequests.Models
{
    public class TMARequest
    {
        [Key]
        public int RequestID { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        public string Group { get; set; }

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
