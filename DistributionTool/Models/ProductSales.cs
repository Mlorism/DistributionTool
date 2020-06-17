using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{	
	/// <summary>
	/// Class model representing product sales.
	/// </summary>
	class ProductSales
	{
		/// <summary>
		/// Price look-up code.
		/// </summary>
		public int PLU { get; set; }
		/// <summary>
		/// Store number.
		/// </summary>
		public int StoreNumber { get; set; }
		/// <summary>
		/// Date of sale.
		/// </summary>
		public DateTime Date { get; set; }
		/// <summary>
		/// Sale quantity.
		/// </summary>
		public int Sales { get; set; }
	}
}
