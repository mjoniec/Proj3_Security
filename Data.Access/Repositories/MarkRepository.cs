using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Access.Repositories
{
    /*internal*/public class MarkRepository : IMarkRepository
    {
        public MarkRepository(/*TODO config*/) { }

        public MarkModel GetById(string id)
        {
            return new MarkModel { X = 3, Y = 4 };
        }

        public IEnumerable<MarkModel> GetAll()
        {
            return new List<MarkModel>
            {
                new MarkModel { X = 2, Y = 1 },
                new MarkModel { X = 3, Y = 4 }
            };
        }
    }
}
