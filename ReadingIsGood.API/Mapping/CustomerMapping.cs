using AutoMapper;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Mapping
{
    public class CustomerMapping : Profile
    {
        public CustomerMapping()
        {
            CreateMap<CustomerResource, Customer>();
            CreateMap<Customer, CustomerResource>();
        }
    }
}
