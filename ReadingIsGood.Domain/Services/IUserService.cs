using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;

namespace ReadingIsGood.Domain.Services
{
    public interface IUserService
    {
        UserResponse AddUser(User user);
        void SaveToken(string userId, string token);
        UserResponse CheckUserLoginInfo(string mail, string password);
        UserResponse FindByEmail(string email);
    }
}
