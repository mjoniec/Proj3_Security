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
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public TokenHandler()
        {
            _tokenOptions = TokenOptions.GetDefault();
            _signingConfigurations = new SigningConfigurations(_tokenOptions.Secret);
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
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

            var accessToken = _jwtSecurityTokenHandler.WriteToken(securityToken);

            return new AccessToken(accessToken, accessTokenExpiration.Ticks);
        }

        private IEnumerable<Claim> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Sub, user.Name),
                new Claim(ClaimTypes.Role, user.Role)
            };

            return claims;
        }
    }
}
