using System;

namespace JwtApi.Model
{
    public class AccessToken : RefreshToken
    {
        public RefreshToken RefreshToken { get; private set; }

        public AccessToken(string token, long expiration, RefreshToken refreshToken)
            : base(token, expiration)
        {
            RefreshToken = refreshToken ?? throw new ArgumentException("Specify a valid refresh token.");
        }
    }
}
