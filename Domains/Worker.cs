using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm.Domains
{
    public class Worker
    {
        public long WorkerId { get; set; }
        public string PassportData { get; set; }
        public decimal Salary { get; set; }
    }
}