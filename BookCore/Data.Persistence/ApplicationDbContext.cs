using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Data.Domain.Entities.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Data.Domain.Entities.Book> Books { get; set; }
        public DbSet<Data.Domain.Entities.Author> Authors { get; set; }
        public DbSet<Data.Domain.Entities.Character> Characters { get; set; }
    }
}
