using AutoMapper;
using ReadingIsGood.API.Resources;
using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.API.Mapping
{
    public class UserMapping : Profile
    {
        public UserMapping()
        {
            CreateMap<UserResource, User>();
            CreateMap<User, UserResource>();
        }
    }
}
