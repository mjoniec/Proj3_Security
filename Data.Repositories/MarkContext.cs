using Data.Model;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class MarkContext : DbContext
    {
        //public MarkContext() : base("connection")
        //{

        //}

        public DbSet<MarkModel> Marks { get; set; }
    }
}
