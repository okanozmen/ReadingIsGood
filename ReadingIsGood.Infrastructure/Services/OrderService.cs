using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;
using System;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly IProductRepository productRepository;
        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository)
        {
            this.orderRepository = orderRepository;
            this.productRepository = productRepository;
        }
        public async Task<OrderResponse> CreateOrder(Order order)
        {
            try
            {
                decimal totalPrice = 0;

                foreach (var item in order.Products)
                {
                    var productDetail = await productRepository.GetProductDetailByIdAsync(item.ProductId);

                    if (productDetail != null)
                    {
                        if (productDetail.Stock < item.Quantity)
                        {
                            if (productDetail.Stock > 0)
                            {
                                return new OrderResponse("There are" + productDetail.Stock + "of this" + item.Name + "in stock");
                            }

                            return new OrderResponse("The" + item.Name + "is not found in stock");
                        }

                        productDetail.Stock = productDetail.Stock - item.Quantity;
                        productRepository.UpdateProductStock(item.ProductId, productDetail.Stock);
                        totalPrice = totalPrice + item.Price;
                    }
                }

                order.TotalPrice = totalPrice;
                order.OrderStatus = OrderStatus.OrderProcessing;
                Guid orderId = Guid.NewGuid();
                order.OrderId = orderId;
                orderRepository.CreateOrderAsync(order);

                return new OrderResponse(order);
            }
            catch (Exception ex)
            {
                return new OrderResponse(ex.Message);
            }
        }

        public async Task<CustomerOrderListResponse> GetOrder(Guid customerId)
        {
            try
            {
                var customerOrder = await orderRepository.GetOrderAsync(customerId);

                if (customerOrder == null)
                {
                    return new CustomerOrderListResponse("Order not found");

                }
                return new CustomerOrderListResponse(customerOrder);
            }
            catch (Exception ex)
            {
                return new CustomerOrderListResponse(ex.Message);
            }
        }

        public async Task<OrderResponse> GetOrderDetail(Guid orderId)
        {
            try
            {
                var orderDetail = await orderRepository.GetOrderDetailAsync(orderId);

                if (orderDetail == null)
                {
                    return new OrderResponse("Order Detail not found");
                }

                return new OrderResponse(orderDetail);
            }
            catch (Exception ex)
            {
                return new OrderResponse(ex.Message);
            }
        }

        public OrderResponse UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            try
            {
                orderRepository.UpdateOrderStatus(orderId, status);
                return new OrderResponse(new Order());
            }
            catch (Exception ex)
            {
                return new OrderResponse(ex.Message);
            }
        }
    }
}
