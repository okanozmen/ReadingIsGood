using AutoMapper;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Mapping
{
    public class OrderMapping : Profile
    {
        public OrderMapping()
        {
            CreateMap<OrderResource, Order>();
            CreateMap<Order, OrderResource>();
        }
    }
}
