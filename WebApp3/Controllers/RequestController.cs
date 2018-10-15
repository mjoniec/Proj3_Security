using Data.Services;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApp3.Controllers
{
    [MobileAppController]
    public class RequestController : ApiController
    {
        IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            _requestService = requestService;
        }

        //[HttpGet("jobs/saveFiles")]
        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody] object requests)
        {
            try
            {
                var message = _requestService.SaveRequests(requests);

                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest("Incorrect requests in body parameter" + e);
            }
        }

        private static IEnumerable<JToken> AllChildren(JToken json)
        {
            foreach (var c in json.Children())
            {
                yield return c;

                foreach (var cc in AllChildren(c))
                {
                    yield return cc;
                }
            }
        }
    }
}