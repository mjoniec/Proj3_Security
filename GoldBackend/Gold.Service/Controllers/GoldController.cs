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
        private readonly IGoldService _goldService;

        /// <summary>
        /// Constructor with gold service data provider (DI instantiated)
        /// </summary>
        /// <param name="goldService"></param>
        public GoldController(IGoldService goldService)
        {
            _goldService = goldService;
        }

        // GET: api/Gold
        /// <summary>
        /// Checks if Gold service has been instantiated. Gives info on MQTT connection status.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult Get()
        {
            if (_goldService == null) return Ok("no gold service");

            return Ok("gold service is ready");
        }

        /// <summary>
        /// Start gold data collection.
        /// </summary>
        /// <remarks>
        /// Action triggers gold data collection process in service. Returns id, which should be passed in following requests returning actual data once it has been obtained internally.
        /// </remarks>
        /// <returns>Request accepted result. Request id to pass in another anction for actual data retrieval.</returns>
        [HttpGet("[action]")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        public IActionResult GetDataPrepared()
        {
            var requestId = _goldService.StartPreparingData();

            return Accepted(requestId);
        }

        // GET: api/Gold/GetData/dataId
        /// <summary>
        /// Daily gold prices
        /// </summary>
        /// <remarks>
        /// Returns collection of paired data: date and gold price in Australian dollars. 
        /// </remarks>
        /// <param name="requestId">Id returned from action initializing data collection. </param>
        /// <returns>Daily gold prices or no content.</returns>
        [HttpGet("[action]/{requestId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult GetData(ushort requestId)
        {
            var allPrices = _goldService.GetDailyGoldPrices(requestId);

            if (allPrices == null) return NoContent();

            return Ok(allPrices);
        }
    }
}
