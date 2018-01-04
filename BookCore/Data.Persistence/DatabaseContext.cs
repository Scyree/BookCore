using Microsoft.EntityFrameworkCore;

namespace Data.Persistance
{
    public class DatabaseContext : DbContext, IDatabaseContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Data.Domain.Entities.Book> Books { get; set; }
        public DbSet<Data.Domain.Entities.Author> Authors { get; set; }
        public DbSet<Data.Domain.Entities.Character> Characters { get; set; }
    }
}
