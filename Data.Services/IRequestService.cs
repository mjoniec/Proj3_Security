using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IRequestService
    {
        string SaveRequests(IEnumerable<Request> requests);
        IEnumerable<Request> GetRequests();
    }
}
