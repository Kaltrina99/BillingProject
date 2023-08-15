using System.Collections.Generic;
using System.Linq;
using BillingSystem.API.Data;
using Microsoft.EntityFrameworkCore;
using WebApi.Entities;
using WebApi.Models;

namespace WebApi.Services
{
    public interface IOrderService
    {
        IEnumerable<OrderResponse> GetAllOrders();
        OrderResponse GetOrderById(int orderId);
        OrderResponse CreateOrder(OrderRequest orderDto);
        OrderResponse UpdateOrder(int orderId, OrderRequest orderDto);
        void DeleteOrder(int orderId);
    }

    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderResponse> GetAllOrders()
        {
            return _context.Orders
                .Select(order => new OrderResponse
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    CustomerName = order.CustomerName,
                    ShippingAddress = order.ShippingAddress
                })
                .ToList();
        }

        public OrderResponse GetOrderById(int orderId)
        {
            var order = _context.Orders.Find(orderId);

            return order != null
                ? new OrderResponse
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    CustomerName = order.CustomerName,
                    ShippingAddress = order.ShippingAddress
                }
                : null;
        }

        public OrderResponse CreateOrder(OrderRequest orderDto)
        {
            var order = new Order
            {
                OrderDate = orderDto.OrderDate,
                CustomerName = orderDto.CustomerName,
                ShippingAddress = orderDto.ShippingAddress
            };

            _context.Orders.Add(order);
            _context.SaveChanges();

            return new OrderResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName,
                ShippingAddress = order.ShippingAddress
            };
        }

        public OrderResponse UpdateOrder(int orderId, OrderRequest orderDto)
        {
            var order = _context.Orders.Find(orderId);

            if (order == null)
                return null;

            order.OrderDate = orderDto.OrderDate;
            order.CustomerName = orderDto.CustomerName;
            order.ShippingAddress = orderDto.ShippingAddress;

            _context.SaveChanges();

            return new OrderResponse
            {
                OrderId = order.OrderId,
                OrderDate = order.OrderDate,
                CustomerName = order.CustomerName,
                ShippingAddress = order.ShippingAddress
            };
        }

        public void DeleteOrder(int orderId)
        {
            var order = _context.Orders.Find(orderId);

            if (order != null)
            {
                _context.Orders.Remove(order);
                _context.SaveChanges();
            }
        }
    }
}
