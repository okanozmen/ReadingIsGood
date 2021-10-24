using ReadingIsGood.Domain.ResponseModels;

namespace ReadingIsGood.Domain.Services
{
    public interface IAuthenticationService
    {
        AccessTokenResponse CreateAccessToken(string email, string password);
    }
}
