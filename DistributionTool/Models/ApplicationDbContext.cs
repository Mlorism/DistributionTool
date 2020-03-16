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

		public ApplicationDbContext()
			: base("name=DefaultConnection")
		{

		}
	}
}
