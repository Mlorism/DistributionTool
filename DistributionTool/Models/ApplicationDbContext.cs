using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DistributionTool.Models
{
	class ApplicationDbContext : DbContext
	{
		public DbSet<UserModel> Users { get; set; }
		public DbSet<ProductModel> Products { get; set; }
		public DbSet<ProductParametersModel> ProductParameters { get; set; }

		public ApplicationDbContext()
			: base("name=DefaultConnection")
		{
			
		}

		protected override void OnModelCreating(DbModelBuilder modelbuilder)
		{
			modelbuilder.Entity<ProductParametersModel>().HasKey(p => new { p.PLU, p.Grade });
		}
	}
}
