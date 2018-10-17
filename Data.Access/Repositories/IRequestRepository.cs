using Data.Access.Model;
using System.Collections.Generic;
using System.Linq;

namespace Data.Access.Repositories
{
    public interface IRequestRepository
    {
        string SaveRequests(IEnumerable<Request> requests);
        IQueryable<Request> GetRequests();
    }
}
