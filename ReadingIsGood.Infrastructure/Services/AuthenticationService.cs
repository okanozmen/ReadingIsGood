using System;
using ReadingIsGood.Domain.Models;
using ReadingIsGood.Domain.ResponseModels;
using ReadingIsGood.Domain.Services;
using ReadingIsGood.Infrastructure.Security;
using IAuthenticationService = ReadingIsGood.Domain.Services.IAuthenticationService;

namespace ReadingIsGood.Infrastructure.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IUserService userService;
        private readonly ITokenHandler tokenHandler;

        public AuthenticationService(IUserService userService, ITokenHandler tokenHandler)
        {
            this.tokenHandler = tokenHandler;
            this.userService = userService;
        }
        public AccessTokenResponse CreateAccessToken(string email, string password)
        {
            UserResponse userResponse = userService.CheckUserLoginInfo(email, password);

            if (userResponse.IsSuccess)
            {
                AccessToken accessToken = tokenHandler.CreateAccessToken(userResponse.user);
                userService.SaveToken(userResponse.user.Id, accessToken.Token);
                return new AccessTokenResponse(accessToken);
            }

            return new AccessTokenResponse(userResponse.Message);
        }
    }
}
