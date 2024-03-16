using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.Services.TMARequest.Models.Dto
{
    public class ItemDto
    {
        public int ItemID { get; set; }

		public string Name { get; set; }

		public string Group { get; set; }

        public string UnitOfMeasurement { get; set; }

        public int Quantity { get; set; }

        public decimal PriceWithoutVAT { get; set; }

        public string Status { get; set; }

        public string StorageLocation { get; set; }

        public string ContactPerson { get; set; }

        public string Photo { get; set; }
    }
}
