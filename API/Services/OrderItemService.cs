using System.Collections.Generic;
using System.Linq;
using BillingSystem.API.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IOrderItemService
    {
        IEnumerable<OrderItemResponse> GetOrderItemsByOrderId(int orderId);
        OrderItemResponse CreateOrderItem(int orderId, OrderItemRequest orderItemDto);
        OrderItemResponse UpdateOrderItem(int orderId, int orderItemId, OrderItemRequest orderItemDto);
        void DeleteOrderItem(int orderItemId);
    }

    public class OrderItemService : IOrderItemService
    {
        private readonly AppDbContext _context;

        public OrderItemService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItemResponse> GetOrderItemsByOrderId(int orderId)
        {
            return _context.OrderItems
                .Where(orderItem => orderItem.OrderId == orderId)
                .Select(orderItem => new OrderItemResponse
                {
                    OrderItemId = orderItem.OrderItemId,
                    ProductId = orderItem.ProductId,
                    Quantity = orderItem.Quantity,
                    Price = orderItem.Price,
                    TotalPrice = orderItem.Quantity * orderItem.Price,
                    Notes = orderItem.Notes
                })
                .ToList();
        }

        public OrderItemResponse CreateOrderItem(int orderId, OrderItemRequest orderItemDto)
        {
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = orderItemDto.ProductId,
                Quantity = orderItemDto.Quantity,
                Price = orderItemDto.Price,
                Notes = orderItemDto.Notes
            };

            _context.OrderItems.Add(orderItem);
            _context.SaveChanges();

            return new OrderItemResponse
            {
                OrderItemId = orderItem.OrderItemId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                TotalPrice = orderItem.Quantity * orderItem.Price,
                Notes = orderItem.Notes
            };
        }

        public OrderItemResponse UpdateOrderItem(int orderId, int orderItemId, OrderItemRequest orderItemDto)
        {
            var orderItem = _context.OrderItems.Find(orderItemId);

            if (orderItem == null || orderItem.OrderId != orderId)
                return null;

            orderItem.ProductId = orderItemDto.ProductId;
            orderItem.Quantity = orderItemDto.Quantity;
            orderItem.Price = orderItemDto.Price;
            orderItem.Notes = orderItemDto.Notes;

            _context.SaveChanges();

            return new OrderItemResponse
            {
                OrderItemId = orderItem.OrderItemId,
                ProductId = orderItem.ProductId,
                Quantity = orderItem.Quantity,
                Price = orderItem.Price,
                TotalPrice = orderItem.Quantity * orderItem.Price,
                Notes = orderItem.Notes
            };
        }

        public void DeleteOrderItem(int orderItemId)
        {
            var orderItem = _context.OrderItems.Find(orderItemId);

            if (orderItem != null)
            {
                _context.OrderItems.Remove(orderItem);
                _context.SaveChanges();
            }
        }
    }
}
