using Data.Access.Model;
using Data.Access.Repositories;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Services
{
    /// <summary>
    /// This service converts JSON into db model
    /// </summary>
    internal class RequestService : IRequestService
    {
        IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public string SaveRequests(object requests)
        {
            //TODO: improve deserialization
            //var requestsParsed = JsonConvert.DeserializeObject<List<Request>>(requests.ToString());

            var requestsParsed = AllChildren(JObject.Parse(requests.ToString()))
                .First(c => c.Type == JTokenType.Array && c.Path.Contains("requests"))
                .Children<JObject>();

            var requestsModels = new List<Request>();

            foreach (var requestParsed in requestsParsed)
            {
                var request = new Request
                {
                    Index = int.Parse(requestParsed.GetValue("ix").ToString()),
                    Name = requestParsed.GetValue("name").ToString(),
                    Visits = null
                };

                var visits = requestParsed.GetValue("visits").ToString();

                if (!string.IsNullOrEmpty(visits)) request.Visits = int.Parse(visits);

                var date = requestParsed.GetValue("date").ToString();
                var dateTime = new DateTime();

                if (DateTime.TryParse(date, out dateTime)) request.Date = dateTime;

                requestsModels.Add(request);
            }

            return _requestRepository.SaveRequests(requestsModels);
        }

        public IEnumerable<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
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
