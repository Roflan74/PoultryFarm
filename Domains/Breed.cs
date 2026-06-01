using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm.Domains
{
    public class Breed
    {
        public long BreedId { get; set; }
        public string Name { get; set; }
        public int AvgEggsPerMonth { get; set; }
        public double AvgWeight { get; set; }
        public int DietNumber { get; set; }
    }
}