
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System.Text;
using Talabat.API.CustomMiddlewares;
using Talabat.API.Factories;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Shared.ErrorModels;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
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

			builder.Services.AddDbContext<StoreIdentityDbContext>(options =>
			options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

			builder.Services.AddScoped<IBasketRepository, BasketRepository>();
			builder.Services.AddScoped<IProductService, ProductService>();
			builder.Services.AddScoped<IBasketService, BasketService>();
			builder.Services.AddScoped<ITokenService, TokenService>();
			builder.Services.AddScoped<IAuthService, AuthService>();

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
			builder.Services.AddAutoMapper(M => M.AddProfile(new ProductProfile()));
			builder.Services.AddAutoMapper(M => M.AddProfile(new BasketProfile()));
			builder.Services.AddAutoMapper(M => M.AddProfile(new AddressProfile()));
			builder.Services.AddScoped<ProductPictureUrlResolver>();

			builder.Services.AddSingleton<IConnectionMultiplexer>((_) =>
			{
				return ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection"));
			});

            builder.Services.AddOpenApi();
			builder.Services.AddSwaggerGen();

			builder.Services.Configure<ApiBehaviorOptions>((options) =>
			{
				options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
			});

			builder.Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			builder.Services.AddAuthentication(configOptions =>
			{
				configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(configOptions => 
				configOptions.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = builder.Configuration["JWT:Issuer"],
					ValidateAudience = true,
					ValidAudience = builder.Configuration["JWT:Audience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
				});

			var app = builder.Build();

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
			// Configure the HTTP request pipeline.
			app.UseMiddleware<CustomExceptionHandlerMiddleware>();

				if (app.Environment.IsDevelopment())
            {

				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseStaticFiles();

			app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
