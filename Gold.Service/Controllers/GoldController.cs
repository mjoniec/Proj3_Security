using Data.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gold.Service.Controllers
{
    /// <summary>
    /// Service for requesting gold data.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        IGoldService _goldService;

        /// <summary>
        /// Constructor with gold service data provider (DI instantiated)
        /// </summary>
        /// <param name="goldService"></param>
        public GoldController(IGoldService goldService)
        {
            _goldService = goldService;
        }

        // GET: api/Gold/GetAll/dataId
        /// <summary>
        /// Daily gold prices
        /// </summary>
        /// <remarks>
        /// Returns collection of paired data: date and gold price in Australlian dollars. 
        /// </remarks>
        /// <param name="dataId">Id returned from action initializing data collection. </param>
        /// <returns></returns>
        [HttpGet("[action]/{dataId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAll(string dataId)
        {
            var allPrices = _goldService.GetDailyGoldPrices(dataId);

            return Ok(allPrices);
        }

        // GET: api/Gold/dataId
        //[HttpGet("{dataId}")]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public IActionResult Get(string dataId)
        //{
        //    var price = _goldService.GetNewestPrice(dataId);

        //    return Ok(price);
        //}

        // GET: api/Gold
        /// <summary>
        /// Start gold data collection.
        /// </summary>
        /// <remarks>
        /// Action triggers gold data collection process in service. Returns id, which should be passed in following requests returning actual data once it has been obtained and allocated in serice.
        /// </remarks>
        /// <returns>Request accepted result. Request id to pass in another anction.</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Get()
        {
            var dataId = _goldService.StartPreparingData();

            return Accepted(dataId);
        }
    }
}
