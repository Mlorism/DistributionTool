using DistributionTool.Enumerators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
	/// <summary>
	/// Class model representing product grade.
	/// </summary>
	class StoreGrade
	{	
		/// <summary>
		/// Store unique number.
		/// </summary>
		public int StoreNumber { get; set; }
		/// <summary>
		/// Store grade is assign to a specific ProductGroup.
		/// </summary>
		public ProductGroupEnum Group { get; set; }
		/// <summary>
		/// Product Group Grade.
		/// </summary>
		public StoreGradeEnum Grade { get; set; }

	}
}
