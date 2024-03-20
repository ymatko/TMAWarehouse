using TMAWarehouse.Services.TMARequest.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests.Models.Dto
{
    public class TMARequestRowDto
    {
        public int RequestRowID { get; set; }

        public int RequestID { get; set; }
        public TMARequestDto? TMARequest { get; set; }

        public int ItemID { get; set; }
        public ItemDto? Item { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string? Comment { get; set; }
    }
}
