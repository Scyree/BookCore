using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Data.Persistence;
using Data.Domain.Entities;
using Business.Services;
using Data.Domain.Interfaces.Services;
using Data.Domain.Interfaces.Repositories;
using Business.Repositories;
using Middleware.Interfaces;
using Middleware.Services;

namespace Presentation
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            const string connection = @"Server = .\SQLEXPRESS; Database = BookCore.Development; Trusted_Connection = true;";
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));

            //Facebook service
            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            //Google service
            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            //Twitter service
            services.AddAuthentication().AddTwitter(twitterOptions =>
            {
                twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            });
            
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IApplicationUserServices, ApplicationUserServices>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<ICharacterRepository, CharacterRepository>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IGenreService, GenreService>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IReviewService, ReviewService>();
            services.AddTransient<IWorkingWithFiles, WorkingWithFiles>();
            services.AddTransient<IAuthorGeneralUsage, AuthorGeneralUsage>();
            services.AddTransient<IBookGeneralUsage, BookGeneralUsage>();
            services.AddTransient<IAuthorBookService, AuthorBookService>();

            //Repos
            services.AddTransient<IBooksForMoodRepository, BooksForMoodRepository>();
            services.AddTransient<IBuyingSiteRepository, BuyingSiteRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IFavoriteRepository, FavoriteRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<IRatingRepository, RatingRepository>();
            services.AddTransient<IRecommandationRepository, RecommandationRepository>();
            services.AddTransient<IReviewRepository, ReviewRepository>();      
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<IAuthorBookRepository, AuthorBookRepository>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder();

            if (env.IsDevelopment())
            {
                builder.AddUserSecrets<Startup>();
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
