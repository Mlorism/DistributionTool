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
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductParameters> ProductParameters { get; set; }

		public ApplicationDbContext()
			: base("name=DefaultConnection")
		{
			
		}

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ProductParameters>().HasKey( p => new { p.PLU, p.Grade });
		}
	}
}
