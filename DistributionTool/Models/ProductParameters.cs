using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using DistributionTool.Enumerators;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing product parameters.
	/// </summary>
	class ProductParameters
	{
		/// <summary>
		/// Price look-up code.
		/// </summary>			
		public int PLU { get; set; }
		/// <summary>
		/// Store grade.
		/// </summary>		
		public StoreGradeEnum Grade { get; set; }
		/// <summary>
		/// Minimum quantity for the grade.
		/// </summary>
		public int Min { get; set; }
		/// <summary>
		/// Maximum quantity for the grade.
		/// </summary>
		public int Max { get; set; }
		/// <summary>
		/// Minimum cover for the grade.
		/// </summary>
		public int Cover { get; set; }
	}
}
