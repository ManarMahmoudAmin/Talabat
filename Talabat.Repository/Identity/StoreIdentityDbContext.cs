using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities.IdentityModule;


namespace Talabat.Repository.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<AppUser>
    {
		public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> options)
			: base(options)
		{

		}
		
	}
}
