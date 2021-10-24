using ReadingIsGood.Domain.Models;

namespace ReadingIsGood.Infrastructure.Security
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);
    }
}
