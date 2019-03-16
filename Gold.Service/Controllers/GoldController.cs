using System.Collections.Generic;
using Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace Gold.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        IGoldService _goldService;

        public GoldController(IGoldService goldService)
        {
            _goldService = goldService;
        }

        // GET: api/Gold
        [HttpGet]
        public IEnumerable<string> Get()
        {
            //return new string[] { string.Empty };

            var price = _goldService.GetNewestPrice();

            return new string[] { price.ToString() };
        }

        // POST: api/Gold
        [HttpPost]
        public IEnumerable<string> Post([FromBody] string value)
        {
            var price = _goldService.GetNewestPrice();

            return new string[] { price.ToString() };
        }
    }
}
