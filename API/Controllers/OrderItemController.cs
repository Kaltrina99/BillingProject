using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Services;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderItemController : ControllerBase
    {
        private IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }

        [HttpGet("order/{orderId}")]
        public IActionResult GetOrderItemsByOrderId(int orderId)
        {
            var orderItems = _orderItemService.GetOrderItemsByOrderId(orderId);
            return Ok(orderItems);
        }

        [HttpPost]
        public IActionResult CreateOrderItem(OrderItemRequest orderItemDto)
        {
            var createdOrderItem = _orderItemService.CreateOrderItem(orderItemDto.OrderId,orderItemDto);
            return CreatedAtAction(nameof(GetOrderItemsByOrderId), new { orderId = createdOrderItem.OrderId }, createdOrderItem);
        }

        [HttpPut("{orderItemId}")]
        public IActionResult UpdateOrderItem(int orderItemId, OrderItemRequest orderItemDto)
        {
            var updatedOrderItem = _orderItemService.UpdateOrderItem(orderItemDto.OrderId,orderItemId, orderItemDto);

            if (updatedOrderItem == null)
                return NotFound();

            return Ok(updatedOrderItem);
        }

        [HttpDelete("{orderItemId}")]
        public IActionResult DeleteOrderItem(int orderItemId)
        {
            _orderItemService.DeleteOrderItem(orderItemId);
            return NoContent();
        }
    }
}
