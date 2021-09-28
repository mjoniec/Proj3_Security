using JwtApi.Model;
using JwtApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JwtApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthenticationService _authenticationService;

        public UserController(UserService userService, AuthenticationService authenticationService)
        {
            _userService = userService;
            _authenticationService = authenticationService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAsync();

            return Ok(users);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] User user)
        {
            var result = await _userService.CreateAsync(user);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userInSystemToVerifyAgainst = await _userService.GetByNameAsync(user.Name);
            var accessToken = await _authenticationService.CreateAccessTokenAsync(userInSystemToVerifyAgainst, user.Password);
            
            return Ok(accessToken);
        }
    }
}
