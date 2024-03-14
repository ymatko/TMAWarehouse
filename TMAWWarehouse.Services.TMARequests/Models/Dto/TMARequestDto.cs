using System.ComponentModel.DataAnnotations;

namespace TMAWWarehouse.Services.TMARequests.Models.Dto
{
    public class TMARequestDto
    {
        public int RequestID { get; set; }

        public string EmployeeName { get; set; }

        public string Group { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
