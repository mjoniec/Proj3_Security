using Data.Access.Model;
using Data.Services;
using Microsoft.Azure.Mobile.Server.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
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
        //public IHttpActionResult Post()
        public IHttpActionResult Post([FromBody] object requests)
        {
            JObject jObject;

            try
            {
                jObject = JObject.Parse(requests.ToString());

                //var r = JsonConvert.DeserializeObject<List<Request>>(requests.ToString());

                var requestsParsed = AllChildren(JObject.Parse(requests.ToString()))
                    .First(c => c.Type == JTokenType.Array && c.Path.Contains("requests"))
                    .Children<JObject>();

                var first = requestsParsed.First();

                var r = first.GetValue("ix");

                var rr = r.ToString();

                //Request request = first.ToObject<Request>();

                var request = new Request
                {
                    Index = int.Parse(first.GetValue("ix").ToString()),
                    Name = first.GetValue("name").ToString(),
                    Date = DateTime.Now
                };

                var message = _requestService.SaveRequests(new List<Request> { request });

                return Ok(message);

            }
            catch(Exception e)
            {
                return BadRequest("Incorrect requests in body parameter");
            }

            //var jObjects = AllChildren(jObject);
            //var r = jObjects.First();

            

            //JObject rr = r[0] as JObject;
            //Request request = rr.ToObject<Request>();


            //foreach(var rr in r)
            //{
            //    rr
            //}


            //dynamic obj = Newtonsoft.Json.JsonConvert.DeserializeObject(requests);

            //var requestsParsed = AllChildren(JObject.Parse(requests))
            //    .First(c => c.Type == JTokenType.Array && c.Path.Contains("requests"))
            //    .Children<JObject>();


            //var message = _requestService.SaveRequests(new List<Request> { new Request { Index = 5, Name = "Test Sii task", Visits = null, Date = DateTime.Now } });

            //return Ok(message);
        }

        // recursively yield all children of json
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