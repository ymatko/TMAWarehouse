namespace TMAWarehouse.Web.Models.Dto
{
    public class TMARequestDto
    {
        public int RequestID { get; set; }

        public string EmployeeName { get; set; }

        public int ItemID { get; set; }

        public ItemDto? Item { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Comment { get; set; }

        public string Status { get; set; }
    }
}
