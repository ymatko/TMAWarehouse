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
    }
}
