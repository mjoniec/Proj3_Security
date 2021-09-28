using JwtApi.Model;
using System.Threading.Tasks;

namespace JwtApi.Services
{
    public class AuthenticationService
    {
        private readonly TokenHandler _tokenHandler = new TokenHandler();

        public async Task<AccessToken> CreateAccessTokenAsync(User userInSystemToVerifyAgainst, string password)
        {
            if (!Utils.PasswordHasher.PasswordMatches(password, userInSystemToVerifyAgainst.Password))
            {
                return null;
            }

            var accessToken = _tokenHandler.CreateAccessToken(userInSystemToVerifyAgainst);

            return accessToken;
        }
    }
}
