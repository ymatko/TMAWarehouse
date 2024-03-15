using System.Runtime.CompilerServices;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;

namespace TMAWarehouse.Web.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> GetAllOrdersAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/Lists/Orderes/GetAll"
            });
        }

        public async Task<ResponseDto?> GetOrderAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.OrderAPIBase + "/Lists/Orderes/Get/" + id
            });
        }

        public async Task<ResponseDto?> CreateOrderAsync(TMARequestDto tMARequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = tMARequestDto,
                Url = SD.OrderAPIBase + "/Lists/Orderes/CreateTMARequest"
            });
        }

        public async Task<ResponseDto?> UpdateOrderAsync(TMARequestDto tMARequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = tMARequestDto,
                Url = SD.OrderAPIBase + "/Lists/Orderes/UpdateTMARequest"
            });
        }

        public async Task<ResponseDto?> DeleteOrderAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.OrderAPIBase + "/Lists/Orderes/DeleteTMARequest/" + id
            });
        }
    }
}
