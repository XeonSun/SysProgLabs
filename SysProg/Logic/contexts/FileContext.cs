using Logic.models;
using System.Data.Entity;


namespace Logic.contexts
{
    class FileContext : DbContext
    {
        public FileContext()
            : base("DbConnection")
        { }

        public DbSet<File> Files { get; set; }
    }
}
