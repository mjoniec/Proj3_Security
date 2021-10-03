using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProtectedController : ControllerBase
    {
        [HttpGet("[action]")]
        [Authorize]//any user no matter what role will get authorised
        public IActionResult GetProtectedDataForAnyUser()
        {
            return Ok("Hello world from protected controller for any role logged user.");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "admin, normal")]
        public IActionResult GetProtectedDataForAdminOrNormalUser()
        {
            return Ok("Hello user with role!.");
        }

        [HttpGet("[action]")]
        [Authorize(Roles = "admin")]
        public IActionResult GetProtectedDataForAdmin()
        {
            return Ok("Hello admin!.");
        }
    }
}
