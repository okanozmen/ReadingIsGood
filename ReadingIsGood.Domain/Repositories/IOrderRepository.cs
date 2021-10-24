using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.Repositories
{
    public interface IOrderRepository
    {
        void CreateOrderAsync(Order order);
        Task<List<Order>> GetOrderAsync(Guid customerId);
        Task<Order> GetOrderDetailAsync(Guid orderId);
        void UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
