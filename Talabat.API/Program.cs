using Talabat.API.Extensions;
using Talabat.Repository;
using Talabat.Service.Extensions;

namespace Talabat.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddApplicationServices();
			builder.Services.AddMappingProfiles();

			builder.Services.AddSwaggerServices();

			builder.Services.AddWebApplicationServices();

			builder.Services.AddIdentityServices(builder.Configuration);

			builder.Services.AddCors(Options =>
			{
				Options.AddPolicy("MyPolicy", op =>
				{
					op.AllowAnyHeader();
					op.AllowAnyMethod();
					op.WithOrigins(builder.Configuration["FrontBaseUrl"]);
				});
			});

			var app = builder.Build();
			await app.SeedDataAsync();
			
			// Configure the HTTP request pipeline.
			app.UseCustomExceptionMiddlewares();

				if (app.Environment.IsDevelopment())
            {

				app.UseSwaggerMiddlewares();
			}
			app.UseStaticFiles();

			app.UseCors("MyPolicy");
			app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
