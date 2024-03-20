using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Reflection;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;
using TMAWarehouse.Web.Utility;

namespace TMAWarehouse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;
        public OrderController(IOrderService orderService, IItemService itemService)
        {
            _orderService = orderService;
            _itemService = itemService;
        }
        [HttpGet]
        public IActionResult GetAll()
        {
			List<TMARequestDto> list;
			ResponseDto? response = _orderService.GetAllOrdersAsync().GetAwaiter().GetResult();
			if (response != null && response.IsSuccess)
			{
				list = JsonConvert.DeserializeObject<List<TMARequestDto>>(Convert.ToString(response.Result));
			}
            else
            {
                list = new List<TMARequestDto>();
            }
			return Json(new { data = list });
		}
        public IActionResult OrderIndex()
        {
            return View();
        }
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderCreate()
        {
            ViewBag.Status = SD.Status;
            ViewBag.Units = SD.Units;
            List<ItemDto> items;
            ResponseDto? response = await _itemService.GetAllItemsAsync();
            if (response != null && response.IsSuccess)
            {
                items = JsonConvert.DeserializeObject<List<ItemDto>>(Convert.ToString(response.Result));
            }
            else
            {
                items = new List<ItemDto>();
            }
            ViewBag.Items = items.ConvertAll(u =>
            {
                return new SelectListItem()
                {
                    Text = u.Name,
                    Value = u.ItemID.ToString()
                };
            });
            return View();
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderCreate(TMARequestDto model)
        {
            ModelState.Remove("Comment");
            if (ModelState.IsValid)
            {
                model.Comment = "";
                ResponseDto? response = await _orderService.CreateOrderAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order created successfully";
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }

        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderUpdate(int orderId)
        {
            ResponseDto? response = await _orderService.GetOrderAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                TMARequestDto? model = JsonConvert.DeserializeObject<TMARequestDto>(Convert.ToString(response.Result));
                ViewBag.Status = SD.Status;
                ViewBag.Units = SD.Units;
                List<ItemDto> items;
                ResponseDto? itemResponse = await _itemService.GetAllItemsAsync();
                if (itemResponse != null && itemResponse.IsSuccess)
                {
                    items = JsonConvert.DeserializeObject<List<ItemDto>>(Convert.ToString(itemResponse.Result));
                }
                else
                {
                    items = new List<ItemDto>();
                }
                ViewBag.Items = items.ConvertAll(u =>
                {
                    return new SelectListItem()
                    {
                        Text = u.Name,
                        Value = u.ItemID.ToString()
                    };
                });
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderUpdate(TMARequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _orderService.UpdateOrderAsync(model);
                if (response != null && response.IsSuccess)
                {
                    TempData["success"] = "Order updated successfully";
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderDelete(int orderId)
        {
            ResponseDto? response = await _orderService.GetOrderAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                TMARequestDto? model = JsonConvert.DeserializeObject<TMARequestDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin)]
        public async Task<IActionResult> OrderDelete(TMARequestDto model)
        {
            ResponseDto? response = await _orderService.DeleteOrderAsync(model.RequestID);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Order deleted successfully";
                return RedirectToAction(nameof(OrderIndex));
            }
            return View(model);
        }
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<IActionResult> ConfirmOrder(int orderId)
        {
            ResponseDto? response = await _orderService.GetOrderAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                var order = JsonConvert.DeserializeObject<TMARequestDto>(Convert.ToString(response.Result));
                ResponseDto? itemResponse = await _itemService.GetItemAsync(order.ItemID);
                if (itemResponse != null && itemResponse.IsSuccess)
                {
                    var item = JsonConvert.DeserializeObject<ItemDto>(Convert.ToString(itemResponse.Result));
                    if(item.Quantity < order.Quantity)
                    {
                        TempData["warning"] = "Insufficient quantity of goods";
                        return RedirectToAction(nameof(OrderIndex));
                    }
                    item.Quantity -= order.Quantity;
                    await _itemService.UpdateItemAsync(item);
                    order.Status = SD.Status_Approved;
                    await _orderService.UpdateOrderAsync(order);
                    TempData["success"] = "Order approved successfully";
                    return RedirectToAction(nameof(OrderIndex));
                }
                return NotFound();
            }
            return NotFound();
        }
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<IActionResult> RejectOrder(int orderId)
        {
            ResponseDto? response = await _orderService.GetOrderAsync(orderId);
            if (response != null && response.IsSuccess)
            {
                TMARequestDto? model = JsonConvert.DeserializeObject<TMARequestDto>(Convert.ToString(response.Result));

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = SD.RoleAdmin + "," + SD.RoleCoordinator)]
        public async Task<IActionResult> RejectOrder(TMARequestDto model)
        {
            ResponseDto? response = await _orderService.GetOrderAsync(model.RequestID);
            if (response != null && response.IsSuccess)
            {
                var order = JsonConvert.DeserializeObject<TMARequestDto>(Convert.ToString(response.Result));
                order.Comment = model.Comment;
                order.Status = SD.Status_Rejected;
                await _orderService.UpdateOrderAsync(order);
                return RedirectToAction(nameof(OrderIndex));
            }
            return NotFound();
        }
    }
}
