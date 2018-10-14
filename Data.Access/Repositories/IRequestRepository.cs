using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Access.Repositories
{
    public interface IRequestRepository
    {
        string SaveRequests(IEnumerable<Request> requests);
        IEnumerable<Request> GetRequests();
    }
}
