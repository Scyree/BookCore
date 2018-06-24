using Business.Interfaces;
using Business.Services;
using Domain.Data;
using Domain.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Repositories;
using Service.Interfaces;
using Service.Services;
using IdentityErrorDescriber = ExploreBooks.Extensions.IdentityErrorDescriber;

namespace ExploreBooks
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
                .AddDefaultTokenProviders()
                .AddErrorDescriber<IdentityErrorDescriber>();

            const string connection = @"Server = .\SQLEXPRESS; Database = ExploreBooks.Development; Trusted_Connection = true;";
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


            // Application's repositories within repository layer
            services.AddTransient<IAuthorBookRepository, AuthorBookRepository>();
            services.AddTransient<IAuthorRepository, AuthorRepository>();
            services.AddTransient<IBookRepository, BookRepository>();
            services.AddTransient<IBookListRepository, BookListRepository>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IBookStateRepository, BookStateRepository>();
            services.AddTransient<IGenreBookRepository, GenreBookRepository>();
            services.AddTransient<IGenreRepository, GenreRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
            services.AddTransient<IRecommendationRepository, RecommendationRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IApplicationUserRepository, ApplicationUserRepository>();
            services.AddTransient<IFollowUserRepository, FollowUserRepository>();
            services.AddTransient<IBookListContentRepository, BookListContentRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();


            // Application's services within service layer
            services.AddTransient<IAuthorBookMiddleware, AuthorBookMiddleware>();
            services.AddTransient<IAuthorMiddleware, AuthorMiddleware>();
            services.AddTransient<IBookMiddleware, BookMiddleware>();
            services.AddTransient<ICommentMiddleware, CommentMiddleware>();
            services.AddTransient<IGenreBookMiddleware, GenreBookMiddleware>();
            services.AddTransient<IPostMiddleware, PostMiddleware>();
            services.AddTransient<IGenreMiddleware, GenreMiddleware>();
            services.AddTransient<IWorkingWithFiles, WorkingWithFiles>();
            services.AddTransient<IBookStateMiddleware, BookStateMiddleware>();
            services.AddTransient<IFollowUserMiddleware, FollowUserMiddleware>();
            services.AddTransient<INotificationMiddleware, NotificationMiddleware>();
            services.AddTransient<IWorkingWithDatabase, WorkingWithDatabase>();


            // Application's services within business layer
            services.AddTransient<IAuthorService, AuthorService>();
            services.AddTransient<IBookService, BookService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<ILikeService, LikeService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUtilityService, UtilityService>();
            services.AddTransient<IRecommendationService, RecommendationService>();
            services.AddTransient<IBookListService, BookListService>();

            services.AddTransient<IApplicationUserServices, ApplicationUserServices>();
            services.AddTransient<IApplicationBookLogic, ApplicationBookLogic>();
            services.AddTransient<IApplicationPictureLogic, ApplicationPictureLogic>();
            services.AddTransient<IApplicationFollowLogic, ApplicationFollowLogic>();

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
                app.UseExceptionHandler("/Errors/");
                app.UseStatusCodePagesWithReExecute("/Errors/Error/{0}");
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
