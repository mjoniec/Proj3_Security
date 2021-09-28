using System;

namespace JwtApi.Model
{
    public class RefreshToken
    {
        public string Token { get; protected set; }
        public long Expiration { get; protected set; }

        public RefreshToken(string token, long expiration)
        {
            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Invalid token.");

            Token = token;

            if (expiration <= 0)
                throw new ArgumentException("Invalid expiration.");

            Expiration = expiration;
        }

        public bool IsExpired() => DateTime.UtcNow.Ticks > Expiration;
    }
}
