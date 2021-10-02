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
        private readonly TokenHandler _tokenHandler;

        public UserController(UserService userService, 
            AuthenticationService authenticationService,
            TokenHandler tokenHandler)
        {
            _userService = userService;
            _authenticationService = authenticationService;
            _tokenHandler = tokenHandler;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _userService.GetAsync();

            return Ok(users);
        }

        /// <summary>
        /// Creates user with password
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
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

        /// <summary>
        /// If the login successfull returns ok with access token
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            var userInSystemToVerifyAgainst = await _userService.GetByNameAsync(user.Name);
            var loginOk = _authenticationService.CheckUserLogin(userInSystemToVerifyAgainst, user.Password);

            if (!loginOk)
            {
                return Unauthorized();
            }

            var accessToken = _tokenHandler.CreateAccessToken(userInSystemToVerifyAgainst);

            return Ok(accessToken);
        }
    }
}
