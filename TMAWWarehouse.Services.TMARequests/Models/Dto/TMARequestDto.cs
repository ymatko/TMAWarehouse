using System.ComponentModel.DataAnnotations;
using TMAWarehouse.Services.TMARequest.Models.Dto;

namespace TMAWWarehouse.Services.TMARequests.Models.Dto
{
    public class TMARequestDto
    {
        public int RequestID { get; set; }

        public string EmployeeName { get; set; }

        public string? Status { get; set; }
        public IEnumerable<TMARequestRowDto> TMARequestRows { get; set; }
    }
}
