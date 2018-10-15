using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Services
{
    public interface IRequestService
    {
        string SaveRequests(object requests);
        IEnumerable<Request> GetRequests();
    }
}
