using LoginCookieApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace LoginCookieApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult ForLoggedUsers()
        {
            return View();
        }

        [HttpGet("login")]//not home/login, just login for simplicity
        public IActionResult Login(string returnUrl)
        {
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost("login")]//has to match action in view
        public async Task<IActionResult> LoginSubmit(string user, string password, string returnUrl)
        {
            if(user == "test" && password == "test")
            {
                await HttpContext.SignInAsync(GetClaimsPrincipal(user)); 

                return Redirect(returnUrl);
            }

            return BadRequest("login submit unsuccessful");
        }

        //this is needed for app to know thet the user is verified
        //after positive verification in order for
        //return redirection to secure page to work
        //else it will loop again into login page
        private ClaimsPrincipal GetClaimsPrincipal(string user)
        {
            var claims = new List<Claim>
                {
                    new Claim("user", user),
                    new Claim(ClaimTypes.NameIdentifier, user)
                };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            //authentication ticket
            //to hand each time authentication is needed
            //instead of passing password validation all over again
            //stored in cookies
            return new ClaimsPrincipal(claimsIdentity);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}