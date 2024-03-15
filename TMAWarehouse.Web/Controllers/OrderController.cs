using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TMAWarehouse.Web.Models.Dto;
using TMAWarehouse.Web.Services.IServices;

namespace TMAWarehouse.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        public async Task<IActionResult> OrderIndex()
        {
            IEnumerable<TMARequestDto>? list = Enumerable.Empty<TMARequestDto>();
            ResponseDto? response = await _orderService.GetAllOrdersAsync();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<IEnumerable<TMARequestDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }
        public async Task<IActionResult> OrderCreate()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> OrderCreate(TMARequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _orderService.CreateOrderAsync(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }
        public async Task<IActionResult> OrderUpdate(int orderId)
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
        public async Task<IActionResult> OrderUpdate(TMARequestDto model)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? response = await _orderService.UpdateOrderAsync(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }
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
        public async Task<IActionResult> OrderDelete(TMARequestDto model)
        {
            ResponseDto? response = await _orderService.DeleteOrderAsync(model.RequestID);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(OrderIndex));
            }
            return View(model);
        }
    }
}
