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
		/// Sale qty from last 7 days.
		/// </summary>
		public int SlsLW { get; set; }
		/// <summary>
		/// Sale qty from last 7 to 14 days.
		/// </summary>
		public int SlsLW1 { get; set; }
		/// <summary>
		/// Sale qty from last 14 to 21 days.
		/// </summary>
		public int SlsLW2 { get; set; }
		/// <summary>
		/// Sale qty from last 21 to 28 days.
		/// </summary>
		public int SlsLW3 { get; set; }
	}
}
