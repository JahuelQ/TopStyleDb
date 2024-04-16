using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TopStyleDb.Core.Interfaces;
using TopStyleDb.Models.DTO;
using TopStyleDb.Core.Services;

namespace TopStyleDb.Controllers
{
    [Route("api/order/")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _service;
        private readonly IAuthService _auth;
        public OrderController(IOrderService service, IAuthService auth)
        {
            _service = service;
            _auth = auth;
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        public async Task<IActionResult> CreateOrder(CreateOrderDTO orderDto)
        {
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userToken == null || !int.TryParse(userToken, out int userId))
            {
                return Unauthorized("Invalid token.");
            }

            var newOrder = await _service.CreateOrder(orderDto, userId);
            if (newOrder == null)
            {
                return BadRequest("Unable to create order.");
            }

            return Ok(newOrder);
        }

        [HttpGet]
        [Authorize]
        [Route("get/{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userToken == null || !int.TryParse(userToken, out int userId))
            {
                return Unauthorized("Invalid token.");
            }

            var order = await _service.GetOrderById(id, userId);
            if (order == null)
            {
                return NotFound("Order not found.");
            }

            return Ok(order);
        }

        [HttpGet]
        [Authorize]
        [Route("get")]
        public async Task<IActionResult> GetCurrentUserOrders()
        {
            var userToken = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userToken == null || !int.TryParse(userToken, out int userId))
            {
                return Unauthorized("Invalid token.");
            }

            var orders = await _service.GetCurrentUserOrders(userId);
            if (orders == null || orders.Count == 0)
            {
                return NotFound("No orders found.");
            }
            return Ok(orders);
        }
    }
}
