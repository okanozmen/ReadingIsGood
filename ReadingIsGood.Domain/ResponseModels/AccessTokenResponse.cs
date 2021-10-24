using ReadingIsGood.Domain.Models;
using System;

namespace ReadingIsGood.Domain.ResponseModels
{
    public class AccessTokenResponse : BaseResponse
    {
        public AccessToken accessToken { get; set; }
        public AccessTokenResponse(bool isSuccess, string Message, AccessToken accessToken) : base(isSuccess, Message)
        {
            this.accessToken = accessToken;
        }
        public AccessTokenResponse(AccessToken accessToken) : this(true, String.Empty, accessToken) { }
        public AccessTokenResponse(string message) : this(false, message, null) { }
    }
}
