using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProtectedController : ControllerBase
    {
        [HttpGet("GetProtectedDataForAnyUser")]
        [Authorize]
        public IActionResult GetProtectedDataForAnyUser()
        {
            return Ok("Hello world from protected controller.");
        }

        [HttpGet("GetProtectedDataForAdmin")]
        [Authorize(Roles = "admin")]
        public IActionResult GetProtectedDataForAdmin()
        {
            return Ok("Hello admin!.");
        }
    }
}
