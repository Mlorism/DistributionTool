using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DistributionTool.Models
{
    /// <summary>
    /// Class model representing product.
    /// </summary>
    class Product
    {
        public int PLU {get; set;}
        public string Name { get; set; }
        public int PackSize { get; set; }
        public string Promotion { get; set; }
        public string GroupName { get; set; }
        public string SubGroup { get; set; }
        public int WhFreeQty { get; set; }
        public int WhFreePc { get; set; }
        public int StoresBelowMin { get; set; }
        public float StoresCover { get; set; }
        public float StoresEffectiveCover { get; set; }
        public bool MondayDistribution { get; set; }
        public bool TuesdayDistribution { get; set; }
        public bool WednesdayDistribution { get; set; }
        public bool ThursdayDistribution { get; set; }
        public bool FridayDistribution { get; set; }
        public int DistributionMethod { get; set; }

    }
}


              
              
                
                
               