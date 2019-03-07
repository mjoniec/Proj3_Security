using Data.Model;
using System.Collections.Generic;

namespace Data.Repositories
{
    public interface IMarkRepository
    {
        MarkModel GetById(string id);
        IEnumerable<MarkModel> GetAll();
        IEnumerable<MarkModel> GetAllDemo();
    }
}
