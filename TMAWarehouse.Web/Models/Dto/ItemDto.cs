﻿using System.ComponentModel.DataAnnotations;
using TMAWarehouse.Web.Utility;

namespace TMAWarehouse.Web.Models.Dto
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

        public string? StorageLocation { get; set; }

        public string? ContactPerson { get; set; }

        public string? PhotoUrl { get; set; }
		public string? PhotoLocalPach { get; set; }
        [MaxFileSize(5)]
        [AllowedExtensions(new string[] { ".jpg", ".png" })]
        public IFormFile? Photo { get; set; }
	}
}
