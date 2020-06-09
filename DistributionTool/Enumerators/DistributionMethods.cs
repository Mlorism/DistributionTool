using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Enumerators
{
	public enum DistributionMethods
	{
		/// <summary>
		/// Keep only the minimum. If the number falls below the minimum, distribute the package.
		/// </summary>
		KeepMinimum,
		/// <summary>
		/// Keep at least set cover based on past sales.  
		/// </summary>
		WeeksOfSales,
		/// <summary>
		/// Calculate future sales based on the sales curve for the subgroup.
		/// </summary>
		SubgroupTrend,
		/// <summary>
		/// Calculate future sales based on the sales curve for the group.
		/// </summary>
		GroupTrend,
		/// <summary>
		/// Distribute all remaining stock based on current store stock and grade.
		/// </summary>
		FinalDistribution,

	}
}
