using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
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
        [HttpGet]
        public IActionResult GetAll()
        {
            List<ItemDto> list;
            ResponseDto response = _itemService.GetAllItemsAsync().GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ItemDto>>(Convert.ToString(response.Result));
            }
            else
            {
                list = new List<ItemDto>();
            }
            return Json(new { data = list });
        }
		public IActionResult ItemIndex()
		{
            return View();
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
