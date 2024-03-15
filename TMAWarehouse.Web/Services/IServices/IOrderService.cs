using TMAWarehouse.Web.Models.Dto;

namespace TMAWarehouse.Web.Services.IServices
{
    public interface IOrderService
    {
        Task<ResponseDto?> GetAllOrdersAsync();
        Task<ResponseDto?> GetOrderAsync(int id);
        Task<ResponseDto?> CreateOrderAsync(TMARequestDto tMARequestDto);
        Task<ResponseDto?> UpdateOrderAsync(TMARequestDto tMARequestDto);
        Task<ResponseDto?> DeleteOrderAsync(int id);
    }
}
