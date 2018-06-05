using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Access.Repositories
{
    public interface IMarkRepository
    {
        MarkModel GetById(string id);
        IEnumerable<MarkModel> GetAll();
    }
}
