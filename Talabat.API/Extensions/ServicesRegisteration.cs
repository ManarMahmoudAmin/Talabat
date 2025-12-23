using Microsoft.AspNetCore.Mvc;
using Talabat.API.Factories;

namespace Talabat.API.Extensions
{
	public static class ServicesRegisteration
	{
		public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
		{
			Services.AddOpenApi();
			Services.AddSwaggerGen();
			
			return Services;
		}

		public static IServiceCollection AddWebApplicationServices(this IServiceCollection Services)
		{
			Services.Configure<ApiBehaviorOptions>((options) =>
			{
				options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
			});

			return Services;
		}
	}
}
