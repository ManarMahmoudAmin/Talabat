
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Talabat.API.CustomMiddlewares;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Data.Contexts;
using Talabat.Service.Mapping;
using Talabat.Service.Mapping.Profiles;
using Talabat.Service.Mapping.Resolvers;
using Talabat.Service.Services;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
           

            builder.Services.AddDbContext<StoreDbContext>(options =>
				options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));
			builder.Services.AddScoped<ProductPictureUrlResolver>();

            builder.Services.AddOpenApi();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Create a scope to retrieve scoped services.
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			// Retrieve the StoreDbContext and ILoggerFactory from the service provider.
			var context = services.GetRequiredService<StoreDbContext>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				// Get any pending migrations for the database.
				var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
				// If there are pending migrations, apply them.
				if (pendingMigrations.Any())
					await context.Database.MigrateAsync();

				// Seed the database with initial data.
				await StoreDbContextSeed.SeedAsync(context);
			}
			catch (Exception ex)
			{
				// Log any errors that occur during migration.
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error occurred during migration");
			}
			// Configure the HTTP request pipeline.
			app.UseMiddleware<CustomExceptionHandlerMiddleware>();

				if (app.Environment.IsDevelopment())
            {

				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
