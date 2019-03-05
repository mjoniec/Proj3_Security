using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Mqtt.Client;

namespace MqttTestClient1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly MqttDualTopicClient _mqttDoubleChannelClientAsync;
        private static string ResponseMessage = "x";

        public ValuesController()
        {
            _mqttDoubleChannelClientAsync = new MqttDualTopicClient(
                "localhost", 1883, "ResponseMqttTopic", "RequestMqttTopic", ResponseReceivedHandler);
        }

        //Move this to service and use DI
        public string ResponseReceivedHandler(string message)
        {
            ResponseMessage = message;

            return message;
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
            _mqttDoubleChannelClientAsync.Send(value);

            //return response;
        }

        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { ResponseMessage };
        }
    }
}
