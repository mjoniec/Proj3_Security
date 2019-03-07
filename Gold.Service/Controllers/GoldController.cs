using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mqtt.Client;

namespace Gold.Service.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoldController : ControllerBase
    {
        private readonly MqttDualTopicClient _mqttDoubleChannelClientAsync;
        private static string ResponseMessage = "x";

        public GoldController()
        {
            _mqttDoubleChannelClientAsync = new MqttDualTopicClient(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic", ResponseReceivedHandler);
        }

        // GET: api/Gold
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { ResponseMessage };
        }

        // POST: api/Gold
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _mqttDoubleChannelClientAsync.Send(value);
        }

        //Move this to service and use DI
        public string ResponseReceivedHandler(string message)
        {
            ResponseMessage = message;

            return message;
        }
    }
}
