using TMAWarehouse.Web.Models.Dto;

namespace TMAWarehouse.Web.Services.IServices
{
    public interface IItemService
    {
        Task<ResponseDto?> GetAllItemsAsync();
        Task<ResponseDto?> GetItemAsync(int id);
        Task<ResponseDto?> CreateItemAsync(ItemDto itemDto);
        Task<ResponseDto?> UpdateItemAsync(ItemDto itemDto);
        Task<ResponseDto?> DeleteItemAsync(int id);

    }
}
