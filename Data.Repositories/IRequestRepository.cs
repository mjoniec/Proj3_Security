using Data.Model;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IRequestRepository
    {
        string SaveRequests(IEnumerable<Request> requests);
        IEnumerable<Request> GetRequests();
    }
}
