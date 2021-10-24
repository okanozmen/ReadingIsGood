using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Domain.Repositories
{
    public interface IUserRepository
    {
        void AddUser(User user);
        void SaveToken(string userId, string token);
        User CheckUserLoginInfo(string email, string password);
        User FindByEmail(string email);
    }
}
