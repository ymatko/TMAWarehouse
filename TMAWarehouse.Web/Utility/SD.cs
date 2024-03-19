using Microsoft.AspNetCore.Mvc.Rendering;

namespace TMAWarehouse.Web.Utility
{
    public class SD
    {
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
        public static string ItemAPIBase { get; set; }
        public static string OrderAPIBase { get; set; }
        public static string AuthAPIBase { get; set; }

        // Item Group
        public const string Electronics = "Electronics";
        public const string Clothing = "Clothing";
        public const string Stationery = "Stationery";
        public const string Footwear = "Footwear";
        public const string Appliances = "Appliances";
        public const string Bags = "Bags";

        public static List<SelectListItem> ItemGroup = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Electronics,Value=Electronics},
            new SelectListItem{Text=SD.Clothing,Value=Clothing},
            new SelectListItem{Text=SD.Stationery,Value=Stationery},
            new SelectListItem{Text=SD.Footwear,Value=Footwear},
            new SelectListItem{Text=SD.Appliances,Value=Appliances},
            new SelectListItem{Text=SD.Bags,Value=Bags},
        };

        // Unit of measurement
        public const string Piece = "Piece";
        public const string Pair = "Pair";

        public static List<SelectListItem> Units = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Piece,Value=Piece},
            new SelectListItem{Text=SD.Pair,Value=Pair},
        };

        // Status of Order

        public const string Status_Approved = "Approved";
        public const string Status_Rejected = "Rejected";

        public static List<SelectListItem> Status = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Status_Approved,Value=Status_Approved},
            new SelectListItem{Text=SD.Status_Rejected,Value=Status_Rejected},
        };

        //Role

        public const string RoleAdmin = "ADMIN";
        public const string RoleEmployee = "EMPLOYEE";
        public const string RoleCoordinator = "COORDINATOR";

        public const string TokenCookie = "JWTToken";
    }
}
