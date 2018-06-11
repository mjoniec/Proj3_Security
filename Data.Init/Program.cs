using Data.Access.Contexts;
using Data.Access.Model;
using System.Collections.Generic;

namespace Data.Init
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var markContext = new MarkContext())
            {
                var marks = new List<MarkModel>
                {
                    new MarkModel { Id = "1", X = 2, Y = 1 },
                    new MarkModel { Id = "2", X = 3, Y = 4 }
                };

                markContext.Marks.AddRange(marks);
                markContext.SaveChanges();
            }
        }
    }
}
