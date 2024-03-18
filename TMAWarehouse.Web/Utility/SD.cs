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
        public const string Group1 = "Group 1";
        public const string Group2 = "Group 2";
        public const string Group3 = "Group 3";
        public const string Group4 = "Group 4";
        public const string Group5 = "Group 5";

        public static List<SelectListItem> ItemGroup = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Group1,Value=Group1},
            new SelectListItem{Text=SD.Group2,Value=Group2},
            new SelectListItem{Text=SD.Group3,Value=Group3},
            new SelectListItem{Text=SD.Group4,Value=Group4},
            new SelectListItem{Text=SD.Group5,Value=Group5},
        };

        // Unit of measurement
        public const string Kilogram = "Kilogram(kg)";
        public const string Hectogram = "Hectogram(hg)";
        public const string Decagram = "Decagram(dag)";
        public const string Gram = "Gram(g)";

        public static List<SelectListItem> Units = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Kilogram,Value=Kilogram},
            new SelectListItem{Text=SD.Hectogram,Value=Hectogram},
            new SelectListItem{Text=SD.Decagram,Value=Decagram},
            new SelectListItem{Text=SD.Gram,Value=Gram},
        };

        // Status of Order

        public const string Status_Approved = "Approved";
        public const string Status_Rejected = "Rejected";

        public static List<SelectListItem> Status = new List<SelectListItem>()
        {
            new SelectListItem{Text=SD.Status_Approved,Value=Status_Approved},
            new SelectListItem{Text=SD.Status_Rejected,Value=Status_Rejected},
        };
    }
}
