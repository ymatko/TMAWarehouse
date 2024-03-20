using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TMAWarehouse.Services.TMARequest.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests.Models
{
    public class TMARequestRow
    {
        [Key]
        public int RequestRowID { get; set; }

        [Required]
        [ForeignKey("TMARequest")]
        public int RequestID { get; set; }
        public TMARequest TMARequest { get; set; }

        [Required]
        [ForeignKey("Item")]
        public int ItemID { get; set; }
        [NotMapped]
        public ItemDto? Item { get; set; }

        [Required]
        public string UnitOfMeasurement { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal PriceWithoutVAT { get; set; }

        public string? Comment { get; set; }
    }
}
