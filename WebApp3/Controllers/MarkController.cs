using Data.Services;
using Microsoft.Azure.Mobile.Server.Config;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp3.Controllers
{
    [MobileAppController]
    public class MarkController : ApiController
    {
        IMarkService _markService;

        public MarkController(IMarkService markService)
        {
            _markService = markService;
        }

        // GET 
        //http://localhost:57158/api/mark
        //[HttpGet]
        public /*IHttpActionResult*/ IEnumerable<string> Get()
        {
            var list = _markService.GetAll();

            return list.Select(l => l.X).Cast<string>();
        }

        // GET 
        //http://localhost:57158/api/mark/3
        //[HttpGet]
        public string Get(int id)
        {
            var s = "no value";
            var list = _markService.GetAll();

            try
            {
                s = list.ToList()[id].X.ToString();
            }
            catch
            {

            }

            return s;
        }
    }
}