using JwtApi.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace JwtApi.Services
{
    public class TokenHandler
    {
        private readonly TokenOptions _tokenOptions;
        private readonly SigningConfigurations _signingConfigurations;
        //private readonly ISet<RefreshToken> _refreshTokens = new HashSet<RefreshToken>();

        public TokenHandler()
        {
            _tokenOptions = TokenOptions.GetDefault();
            _signingConfigurations = new SigningConfigurations(_tokenOptions.Secret);
        }

        public AccessToken CreateAccessToken(User user)
        {
            var accessTokenExpiration = DateTime.UtcNow.AddSeconds(_tokenOptions.AccessTokenExpiration);

            var securityToken = new JwtSecurityToken
            (
                issuer: _tokenOptions.Issuer,
                audience: _tokenOptions.Audience,
                claims: GetClaims(user),
                expires: accessTokenExpiration,
                notBefore: DateTime.UtcNow,
                signingCredentials: _signingConfigurations.SigningCredentials
            );

            var handler = new JwtSecurityTokenHandler();
            var accessToken = handler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name)
            };

            //foreach (var userRole in user.UserRoles)
            //{
                claims.Add(new Claim(ClaimTypes.Role, "user"));
            //}

            return claims;
        }
    }
}
