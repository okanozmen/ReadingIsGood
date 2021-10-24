using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.MongoDb;
using ReadingIsGood.Domain.Repositories;
using ReadingIsGood.Infrastructure.MongoDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ReadingIsGood.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoDbContext mongoDbContext;
        public OrderRepository(IMongoDbContext mongoDbContext)
        {
            this.mongoDbContext = mongoDbContext;
        }

        public async void CreateOrderAsync(Order order)
        {
            await mongoDbContext.InsertOneAsync(order, CollectionName.Order.ToString());
        }

        public async Task<List<Order>> GetOrderAsync(Guid customerId)
        {
            var orderList = await mongoDbContext.FindAsync<Customer, Order>(x => x.CustomerId == customerId,CollectionName.Order.ToString());
            return orderList;
        }

        public async Task<Order> GetOrderDetailAsync(Guid orderId)
        {
            var orderDetail = await mongoDbContext.FindAsync<Order, Order>(x => x.OrderId == orderId,CollectionName.Order.ToString());
            return orderDetail.FirstOrDefault();
        }

        public void UpdateOrderStatus(Guid orderId, OrderStatus status)
        {
            Expression<Func<Order, bool>> filter = x => x.OrderId == orderId;
            mongoDbContext.UpdateOne(filter, x => x.OrderStatus, status, CollectionName.Order.ToString());
        }
    }
}
