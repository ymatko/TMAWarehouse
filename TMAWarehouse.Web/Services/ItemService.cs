using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;

namespace TMAWarehouse.Web.Services
{
    public class ItemService : IItemService
    {
        private readonly IBaseService _baseService;
        public ItemService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> GetAllItemsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ItemAPIBase + "/Lists/Items/GetAll"
            });
        }

        public async Task<ResponseDto?> GetItemAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.ItemAPIBase + "/Lists/Items/Get/" + id
            });
        }

        public async Task<ResponseDto?> CreateItemAsync(ItemDto itemDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.POST,
                Data = itemDto,
                Url = SD.ItemAPIBase + "/Lists/Items/CreateItem",
                ContentType = SD.ContentType.MultipartFormData
            });
        }

        public async Task<ResponseDto?> UpdateItemAsync(ItemDto itemDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.PUT,
                Data = itemDto,
                Url = SD.ItemAPIBase + "/Lists/Items/UpdateItem",
				ContentType = SD.ContentType.MultipartFormData
			});
        }

        public async Task<ResponseDto?> DeleteItemAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.ItemAPIBase + "/Lists/Items/DeleteItem/" + id
            });
        }
    }
}
