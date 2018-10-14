using Data.Access.Model;
using Data.Access.Repositories;
using System.Collections.Generic;

namespace Data.Services
{
    internal class RequestService : IRequestService
    {
        IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public string SaveRequests(IEnumerable<Request> requests)
        {
            return _requestRepository.SaveRequests(requests);
        }

        public IEnumerable<Request> GetRequests()
        {
            return _requestRepository.GetRequests();
        }
    }
}
