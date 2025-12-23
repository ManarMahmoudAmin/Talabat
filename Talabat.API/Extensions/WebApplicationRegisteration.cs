using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.API.CustomMiddlewares;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;

namespace Talabat.API.Extensions
{
	public static class WebApplicationRegisteration
	{
		public static async Task SeedDataAsync(this WebApplication app)
		{
			// Create a scope to retrieve scoped services.
			using var scope = app.Services.CreateScope();
			var services = scope.ServiceProvider;

			// Retrieve the StoreDbContext and ILoggerFactory from the service provider.
			var context = services.GetRequiredService<StoreDbContext>();
			var identityContext = services.GetRequiredService<StoreIdentityDbContext>();
			var userManager = services.GetRequiredService<UserManager<AppUser>>();
			var loggerFactory = services.GetRequiredService<ILoggerFactory>();

			try
			{
				// Get any pending migrations for the database.
				var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
				// If there are pending migrations, apply them.
				if (pendingMigrations.Any())
					await context.Database.MigrateAsync();

				var pendingIdentityMigrations = await identityContext.Database.GetPendingMigrationsAsync();
				if (pendingIdentityMigrations.Any())
					await identityContext.Database.MigrateAsync();

				// Seed the database with initial data.
				await StoreDbContextSeed.SeedAsync(context);
				await StoreIdentityDbContextSeed.UserSeedAsync(userManager);
			}
			catch (Exception ex)
			{
				// Log any errors that occur during migration.
				var logger = loggerFactory.CreateLogger<Program>();
				logger.LogError(ex, "An error occurred during migration");
			}
		}

		public static IApplicationBuilder UseCustomExceptionMiddlewares(this IApplicationBuilder app)
		{
			app.UseMiddleware<CustomExceptionHandlerMiddleware>();
			return app;
		}

		public static IApplicationBuilder UseSwaggerMiddlewares(this WebApplication app)
		{
			app.MapOpenApi();
			app.UseSwagger();
			app.UseSwaggerUI();

			return app;
		}

	}
}
