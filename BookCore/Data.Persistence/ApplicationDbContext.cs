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

        public DbSet<Data.Domain.Entities.Author> Authors { get; set; }
        public DbSet<Data.Domain.Entities.Book> Books { get; set; }
        public DbSet<Data.Domain.Entities.BooksForMood> BooksForMoods { get; set; }
        public DbSet<Data.Domain.Entities.BuyingSite> BuyingSites { get; set; }
        public DbSet<Data.Domain.Entities.Character> Characters { get; set; }
        public DbSet<Data.Domain.Entities.Comment> Comments { get; set; }
        public DbSet<Data.Domain.Entities.Detail> Details { get; set; }
        public DbSet<Data.Domain.Entities.Favorite> Favorites { get; set; }
        public DbSet<Data.Domain.Entities.Genre> Genres { get; set; }
        public DbSet<Data.Domain.Entities.Rating> Ratings { get; set; }
        public DbSet<Data.Domain.Entities.Recommandation> Recommandations { get; set; }
        public DbSet<Data.Domain.Entities.Review> Reviews { get; set; }
    }
}
