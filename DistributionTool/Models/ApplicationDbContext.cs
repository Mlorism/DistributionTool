using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;

namespace DistributionTool.Models
{
	class ApplicationDbContext : DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Product> Products { get; set; }
		public DbSet<ProductParameters> ProductParameters { get; set; }
		public DbSet<StoreGrade> StoresGrades { get; set; }
		public DbSet<ProductSales> ProductSales { get; set; }
		public DbSet<ProductStock> ProductStock{ get; set; }
		public DbSet<ProductDistribution> ProductDistribution { get; set; }
		public DbSet<GroupCurve> GroupCurve { get; set; }
		public DbSet<SalesWeek> SalesWeek { get; set; }

		public ApplicationDbContext()
			: base("name=DefaultConnection")
		{
			
		}

		protected override void OnModelCreating(DbModelBuilder modelbuilder)
		{
			modelbuilder.Entity<Product>().Property(p => p.PLU).HasDatabaseGeneratedOption(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.None);
			modelbuilder.Entity<ProductParameters>().HasKey(p => new { p.PLU, p.Grade });			
			modelbuilder.Entity<StoreGrade>().HasKey(p => new { p.StoreNumber, p.Group });
			modelbuilder.Entity<ProductSales>().HasKey(p => new { p.PLU, p.StoreNumber});
			modelbuilder.Entity<ProductStock>().HasKey(p => new { p.PLU, p.StoreNumber });
			modelbuilder.Entity<ProductDistribution>().HasKey(p => new { p.PLU, p.StoreNumber });
			modelbuilder.Entity<GroupCurve>().HasKey(p => new {p.Group, p.Week} );
			modelbuilder.Entity<SalesWeek>().HasKey(p => new {p.Week, p.StartDate });
		}
	}
}
