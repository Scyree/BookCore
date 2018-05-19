using Domain.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Domain.Persistence
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AuthorBook>()
                .HasKey(authorBook => new { authorBook.AuthorId, authorBook.BookId });

            modelBuilder.Entity<GenreBook>()
                .HasKey(genreBook => new { genreBook.GenreId, genreBook.BookId });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BooksForMood> BooksForMoods { get; set; }
        public DbSet<BuyingSite> BuyingSites { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<BookState> BookStates { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<Recommandation> Recommandations { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<AuthorBook> AuthorBooks { get; set; }
        public DbSet<GenreBook> GenreBooks { get; set; }
    }
}
