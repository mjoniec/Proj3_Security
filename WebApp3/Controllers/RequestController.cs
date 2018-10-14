using Data.Access.Model;
using Data.Services;
using Microsoft.Azure.Mobile.Server.Config;
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

        // POST api/data
        //[HttpPost("data")]
        [HttpPost]
        //public IHttpActionResult Post([FromBody] IEnumerable<Request> requests)
        public IHttpActionResult Post()
        {
            var message = _requestService.SaveRequests(new List<Request> { new Request { Index = 5, Name = "Test Sii task", Visits = null, Date = DateTime.Now } });

            return Ok(message);
        }
    }
}