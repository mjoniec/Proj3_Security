using JwtApi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JwtApi.Services
{
    public class UserService
    {
        private readonly List<User> _users;

        public UserService()
        {
            _users = new List<User> 
            { 
                new User
                {
                    Name = "normal",
                    Password = "normal"
                },
                new User
                {
                    Name = "admin",
                    Password = "admin"
                }
            };
        }

        public async Task<bool> CreateUserAsync(User user)
        {
            _users.Add(user);

            return true;
        }
    }
}
