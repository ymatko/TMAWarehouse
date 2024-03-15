using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Newtonsoft.Json;
using System.Reflection;
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
        public async Task<IActionResult> ItemCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ItemCreate(ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _itemService.CreateItemAsync(itemDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ItemIndex));
                }
            }
            return View(itemDto);
        }
        public async Task<IActionResult> ItemUpdate(int itemId)
        {
            ResponseDto? response = await _itemService.GetItemAsync(itemId);
            if (response != null && response.IsSuccess)
            {
                ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ItemUpdate(ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _itemService.UpdateItemAsync(itemDto);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ItemIndex));
                }
            }
            return View(itemDto);
        }
        public async Task<IActionResult> ItemDelete(int itemId)
        {
            ResponseDto? response = await _itemService.GetItemAsync(itemId);
            if (response != null && response.IsSuccess)
            {
                ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ItemDelete(ItemDto model)
        {
            ResponseDto? response = await _itemService.DeleteItemAsync(model.ItemID);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ItemIndex));
            }
            return View(model);
        }

    }
}
