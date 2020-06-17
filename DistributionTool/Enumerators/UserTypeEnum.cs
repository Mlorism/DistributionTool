using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Enumerators
{
	public enum UserTypeEnum
	{
		/// <summary>
		/// Admin can create, delete, update or suspend user account.
		/// </summary>
		Admin,
		/// <summary>
		/// AllocationManager can change allocator permissions.
		/// </summary>
		AllocationManager,
		/// <summary>
		/// Allocator is responsible for creating distributions of stock to stores.
		/// </summary>
		Allocator
	}
}
