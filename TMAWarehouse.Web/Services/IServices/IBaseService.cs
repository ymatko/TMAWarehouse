using TMAWarehouse.Web.Models.Dto;

namespace TMAWarehouse.Web.Services.IServices
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
