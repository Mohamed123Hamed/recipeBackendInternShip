using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Formatters.Xml;
using Microsoft.EntityFrameworkCore;
using RecipeBackEnd.APIs.Dto.Helpers;
using RecipeBackEnd.APIs.Extension;
using RecipeBackEnd.Core.IRepo;
using RecipeBackEnd.Core.Models.identity;
using RecipeBackEnd.Repository;
using RecipeBackEnd.Repository.Data;
using RecipeBackEnd.Repository.Data.identity;

namespace RecipeBackEnd.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Service
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at /* https://aka.ms/aspnetcore/swashbuckle */
             builder.Services.AddEndpointsApiExplorer();
             builder.Services.AddSwaggerGen();

             builder.Services.AddDbContext<StoreContext>(Options =>
            {
                 Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });
             builder.Services.AddScoped<IRecipeBackEnd, RecipeImplement>();
            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            });

            // add user
            builder.Services.AddIdentityServices(builder.Configuration);
            var app = builder.Build();
            #endregion

            #region Update Database
            using var Scope = app.Services.CreateScope();
            var Services = Scope.ServiceProvider;

            var LoggerFactory = Services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbContext = Services.GetRequiredService<StoreContext>(); //Ask clr for create object
                await dbContext.Database.MigrateAsync();  // Update Database

                var IdentityDbcontext = Services.GetRequiredService<AppIdentityDbContext>();
                await IdentityDbcontext.Database.MigrateAsync(); // Update Database

                // create one user seed
                var UserManger = Services.GetRequiredService<UserManager<AppUser>>();
                await AppIdenetityDbContextSeed.SeedUserAsync(UserManger);
            }
            catch (Exception ex)
            {
                var logger = LoggerFactory.CreateLogger<Program>(); //work in program
                logger.LogError(ex,"An Error Occoured During ");  // log error
            }
            #endregion

            #region Configures
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                 app.UseSwagger();
                 app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();
            #endregion
            //app.Run();
            app.Run();
        }
    }
}