using System.ComponentModel.DataAnnotations;

namespace TMAWarehouse.Web.Models.Dto
{
    public class TMARequestDto
    {
        public int RequestID { get; set; }

        public string EmployeeName { get; set; }

        public string? Status { get; set; }
        public IEnumerable<TMARequestRowDto> TMARequestRows { get; set; }
    }
}
