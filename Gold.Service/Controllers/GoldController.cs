using Data.Services;
using Microsoft.AspNetCore.Http;
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

        // GET: api/Gold/GetAll/dataId
        [HttpGet("[action]/{dataId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll(string dataId)
        {
            var allPrices = _goldService.GetAll(dataId);

            return Ok(allPrices);
        }

        // GET: api/Gold/dataId
        [HttpGet("{dataId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(string dataId)
        {
            var price = _goldService.GetNewestPrice(dataId);

            return Ok(price);
        }

        // GET: api/Gold
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Get()
        {
            var dataId = _goldService.StartPreparingData();

            return Accepted(dataId);
        }
    }
}
