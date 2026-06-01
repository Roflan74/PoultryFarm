using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm.Domains
{
    public class Cage
    {
        public long CageId { get; set; }
        public int ShopNumber { get; set; } // Номер цеха
        public int RowNumber { get; set; }  // Номер ряда
        public int CageNumber { get; set; } // Номер клетки в ряду
        public long? WorkerId { get; set; } // ID закрепленного работника
    }
}