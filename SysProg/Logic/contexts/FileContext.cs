using Logic.models;
using System.Data.Entity;


namespace Logic.contexts
{
    public class FileContext : DbContext
    {
        public FileContext()
            : base("DBConnection")
        { }

        public DbSet<File> Files { get; set; }
    }
}
