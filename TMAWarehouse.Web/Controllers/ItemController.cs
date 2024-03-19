using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;
using static TMAWarehouse.Web.Utility.SD;

namespace TMAWarehouse.Web.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _contextAccessor;
        public ItemController(IItemService itemService, IOrderService orderService, IHttpContextAccessor contextAccessor)
        {
            _itemService = itemService;
            _orderService = orderService;
            _contextAccessor = contextAccessor;
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
        [Authorize(Roles = SD.RoleEmployee)]
        public IActionResult ItemIndexHome()
		{
			return View();
		}

        [Authorize(Roles = SD.RoleEmployee)]
        public async Task<IActionResult> MakeOrder(int itemId)
        {
            ResponseDto? response = await _itemService.GetItemAsync(itemId);
            if (response != null && response.IsSuccess)
            {
                ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));
                model.ContactPerson = string.Empty;
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = SD.RoleEmployee)]
        public async Task<IActionResult> MakeOrder(ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                TMARequestDto? newOrder = new()
                {
                    EmployeeName = _contextAccessor.HttpContext.User.Identity.Name,
                    ItemID = itemDto.ItemID,
                    UnitOfMeasurement = itemDto.UnitOfMeasurement,
                    Quantity = itemDto.Quantity,
                    PriceWithoutVAT = itemDto.PriceWithoutVAT,
                    Comment = itemDto.ContactPerson,
                    Status = "New"
                };
                ResponseDto? response = await _orderService.CreateOrderAsync(newOrder);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Request created successfully";
                    return RedirectToAction("ItemIndexHome");
                }
            }
            return NotFound();
        }

        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ItemCreate()
        {
            ViewBag.ItemGroup = SD.ItemGroup;
            ViewBag.Units = SD.Units;
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ItemCreate(ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _itemService.CreateItemAsync(itemDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Item created successfully";
                    return RedirectToAction(nameof(ItemIndex));
                }
            }
            return View(itemDto);
        }
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ItemUpdate(int itemId)
        {
            ResponseDto? response = await _itemService.GetItemAsync(itemId);
            if (response != null && response.IsSuccess)
            {
                ItemDto? model = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(response.Result));
                ViewBag.ItemGroup = SD.ItemGroup;
                ViewBag.Units = SD.Units;
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ItemUpdate(ItemDto itemDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _itemService.UpdateItemAsync(itemDto);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Item updated successfully";
                    return RedirectToAction(nameof(ItemIndex));
                }
            }
            return View(itemDto);
        }
        [Authorize(Roles = SD.RoleAdmin)]
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
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> ItemDelete(ItemDto model)
        {
            ResponseDto? response = await _itemService.DeleteItemAsync(model.ItemID);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Item deleted successfully";
                return RedirectToAction(nameof(ItemIndex));
            }
            return View(model);
        }

    }
}
