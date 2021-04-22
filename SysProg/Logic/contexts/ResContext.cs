using Logic.models;
using System.Data.Entity;

namespace Logic.contexts
{
    public class ResContext : DbContext
    {
        public ResContext()
            : base("DbConnection")
        { }

        public DbSet<Resource> Resources { get; set; }
    }
}
