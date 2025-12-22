
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
using Talabat.API.Extensions;
using Talabat.API.Factories;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Core.Shared.ErrorModels;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Repository.Repositories;
using Talabat.Service.Extensions;
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
			builder.Services.AddInfrastructureServices(builder.Configuration);
			builder.Services.AddApplicationServices();
			builder.Services.AddMappingProfiles();

			builder.Services.AddSwaggerServices();

			builder.Services.AddWebApplicationServices();

			builder.Services.AddIdentityServices(builder.Configuration);

			var app = builder.Build();
			await app.SeedDataAsync();
			
			// Configure the HTTP request pipeline.
			app.UseCustomExceptionMiddlewares();

				if (app.Environment.IsDevelopment())
            {

				app.UseSwaggerMiddlewares();
			}
			app.UseStaticFiles();

			app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
