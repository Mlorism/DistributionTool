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
	class StoreGradeModel
	{	
		[Key]
		public int StoreNumber { get; set; }
		public StoreGradeModel Grade { get; set; }
		//public  MyProperty { get; set; }
	}
}
