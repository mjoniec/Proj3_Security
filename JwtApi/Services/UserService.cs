using JwtApi.Model;
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
                    Password = Utils.PasswordHasher.HashPassword("normal"),
                    Role = Role.normal.ToString()
                },
                new User
                {
                    Name = "admin",
                    Password = Utils.PasswordHasher.HashPassword("admin"),
                    Role = Role.admin.ToString()
                }
            };
        }

        public async Task<IEnumerable<string>> GetAsync()
        {
            return _users.Select(u => u.Name);
        }

        public async Task<User> GetByNameAsync(string name)
        {
            return _users.FirstOrDefault(u => u.Name == name);
        }

        public async Task<bool> CreateAsync(User user)
        {
            user.Password = Utils.PasswordHasher.HashPassword(user.Password);
            user.Role = Role.normal.ToString();

            _users.Add(user);

            return true;
        }
    }
}
