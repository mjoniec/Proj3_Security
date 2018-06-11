using Data.Access.Contexts;
using Data.Access.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Access.Repositories
{
    /*internal*/public class MarkRepository : IMarkRepository
    {
        MarkContext _markContext;

        public MarkRepository(MarkContext markContext/*TODO config*/)
        {
            _markContext = markContext;
        }

        public MarkModel GetById(string id)
        {
            return _markContext.Marks.Find(id);

            //return _markContext.Marks.ToList().FirstOrDefault(mark => mark.X == Int32.Parse(id));

            //return new MarkModel { X = 3, Y = 4 };
        }

        public IEnumerable<MarkModel> GetAll()
        {
            return _markContext.Marks.ToList();

            //return new List<MarkModel>
            //{
            //    new MarkModel { X = 2, Y = 1 },
            //    new MarkModel { X = 3, Y = 4 }
            //};
        }
    }
}
