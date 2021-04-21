using Logic.models;
using System.Data.Entity;

namespace Logic.contexts
{
    public class ResourceContext : DbContext
    {
        public ResourceContext()
            : base("DbConnection")
        { }

        public DbSet<Resource> Resources { get; set; }
    }
}
