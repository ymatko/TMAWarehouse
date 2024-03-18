using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.Services.Item.Models
{
    public class Item
    {
        [Key]
        public int ItemID { get; set; }
		[Required]
        public string Name { get; set; }

		[Required]
        public string Group { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceWithoutVAT { get; set; }

        [Required]
        public string Status { get; set; }

        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }

        public string? Photo { get; set; }
    }
}
