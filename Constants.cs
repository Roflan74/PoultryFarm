using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm
{
    public static class Constants
    {
        public static class Tables
        {
            // Здесь перечисляем реальные названия таблиц из нашей SQL-базы
            public const string Breed = "Breeds";
            public const string Cage = "Cages";
            public const string Worker = "Workers";
            public const string Chicken = "Chickens";
        }
    }
}