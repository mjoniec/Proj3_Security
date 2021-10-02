using JwtApi.Model;

namespace JwtApi.Services
{
    public class AuthenticationService
    {
        public bool CheckUserLogin(User userInSystemToVerifyAgainst, string password)
        {
            return Utils.PasswordHasher.PasswordMatches(password, userInSystemToVerifyAgainst.Password);
        }
    }
}
