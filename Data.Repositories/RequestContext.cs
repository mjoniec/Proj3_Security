using Data.Model;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class RequestContext : DbContext
    {
        private const string V = "XmlTest";

        //public RequestContext() : base(V)
        //{

        //}

        public DbSet<Request> Requests { get; set; }
    }
}
