using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<Domain.Entities.ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Domain.Entities.Author> Authors { get; set; }
        public DbSet<Domain.Entities.Book> Books { get; set; }
        public DbSet<Domain.Entities.BooksForMood> BooksForMoods { get; set; }
        public DbSet<Domain.Entities.BuyingSite> BuyingSites { get; set; }
        public DbSet<Domain.Entities.Character> Characters { get; set; }
        public DbSet<Data.Domain.Entities.Comment> Comments { get; set; }
        public DbSet<Domain.Entities.Favorite> Favorites { get; set; }
        public DbSet<Domain.Entities.Genre> Genres { get; set; }
        public DbSet<Domain.Entities.Rating> Ratings { get; set; }
        public DbSet<Domain.Entities.Recommandation> Recommandations { get; set; }
        public DbSet<Domain.Entities.Review> Reviews { get; set; }
        public DbSet<Domain.Entities.Like> Likes { get; set; }
    }
}
