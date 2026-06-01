using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm.Domains
{
    public class Chicken
    {
        public long ChickenId { get; set; }
        public double Weight { get; set; }
        public int Age { get; set; }
        public int EggsPerMonth { get; set; }

        // Внешние ключи для связи со справочниками
        public long? BreedId { get; set; }
        public long? CageId { get; set; }
    }
}