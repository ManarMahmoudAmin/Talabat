using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.Core.Entities.IdentityModule;
using Talabat.Repository.Identity;

namespace Talabat.API.Extensions
{
	public static class IdentityServicesExension
	{
		public static IServiceCollection AddIdentityServices(this IServiceCollection Services, IConfiguration Configuration)
		{
			Services.AddIdentity<AppUser, IdentityRole>()
				.AddEntityFrameworkStores<StoreIdentityDbContext>();

			Services.AddAuthentication(configOptions =>
			{
				configOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
				configOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			})
				.AddJwtBearer(configOptions =>
				configOptions.TokenValidationParameters = new TokenValidationParameters()
				{
					ValidateIssuer = true,
					ValidIssuer = Configuration["JWT:Issuer"],
					ValidateAudience = true,
					ValidAudience = Configuration["JWT:Audience"],
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"])),
				});

			return Services;
		}
	}
}
