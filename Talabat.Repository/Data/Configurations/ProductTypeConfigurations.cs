using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Repository.Data.Configurations
{
	class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
	{
		public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<ProductType> builder)
		{
			builder.Property(T => T.Name)
				.IsRequired();
		}
	}
}
