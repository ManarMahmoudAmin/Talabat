using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Data.Dtos;
using Talabat.Core.Entities;

namespace Talabat.Service.Mapping.Resolvers
{
	public class ProductPictureUrlResolver : IValueResolver<Product, ProductDto, string>
	{
		private readonly IConfiguration _configuration;

		public ProductPictureUrlResolver(IConfiguration configuration)
		{
			_configuration = configuration;
		}
		public string Resolve(Product source, ProductDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.PictureUrl))
				return $"{_configuration["ApiBaseUrl"]}/{source.PictureUrl}";
			else 
				return string.Empty;
		}
	}
}
