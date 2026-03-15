using Microsoft.Extensions.DependencyInjection;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Services.Contract;
using Talabat.Repository.Repositories;
using Talabat.Service.Mapping.Profiles;
using Talabat.Service.Mapping.Resolvers;
using Talabat.Service.Services;

namespace Talabat.Service.Extensions
{
    public static class ApplicationServicesRegisteration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
			Services.AddScoped<IProductService, ProductService>();
			Services.AddScoped<IBasketService, BasketService>();
			Services.AddScoped<ITokenService, TokenService>();
			Services.AddScoped<IAuthService, AuthService>();
			Services.AddScoped<IOrderService, OrderService>();
			Services.AddScoped<IPaymentService, PaymentService>();

			return Services;
		}

		public static IServiceCollection AddMappingProfiles(this IServiceCollection Services)
		{
			Services.AddAutoMapper(cfg =>
			{
				cfg.AddProfile<ProductProfile>();
				cfg.AddProfile<BasketProfile>();
				cfg.AddProfile<AddressProfile>();
				cfg.AddProfile<OrderProfile>();
			});

			Services.AddScoped<ProductPictureUrlResolver>();
			Services.AddScoped<OrderItemPictureUrlResolver>();

			return Services;
		}

	}
}
