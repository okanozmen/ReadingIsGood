using System;
using System.Threading.Tasks;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;

namespace ReadingIsGood.Domain.Services
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(Order order);
        Task<CustomerOrderListResponse> GetOrder(Guid customerId);
        Task<OrderResponse> GetOrderDetail(Guid orderId);
        OrderResponse UpdateOrderStatus(Guid orderId, OrderStatus status);
    }
}
