using Data.Services;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace WebApp.Controllers
{
    public class MarkController : ApiController
    {
        IMarkService _markService;

        public MarkController(IMarkService markService)
        {
            _markService = markService;
        }

        // GET 
        //http://localhost:57158/api/mark
        public IEnumerable<string> Get()
        {
            var list = _markService.GetAll();

            return list.Select(l => l.X).Cast<string>();
        }

        // GET 
        //http://localhost:57158/api/mark/3
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