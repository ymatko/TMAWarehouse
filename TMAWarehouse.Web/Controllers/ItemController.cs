using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;

namespace TMAWarehouse.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }
        public async Task<IActionResult> ItemIndex()
        {
            IEnumerable<ItemDto>? list = Enumerable.Empty<ItemDto>();
            ResponseDto? response = await _itemService.GetAllItemsAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<IEnumerable<ItemDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
    }
}
