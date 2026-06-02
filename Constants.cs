namespace PoultryFarm
{
    public static class Constants
    {
        public static class Tables
        {
            // Справочники (dict) — 5 таблиц
            public const string Diet = "Diets";
            public const string Breed = "Breeds";
            public const string Shop = "Shops";
            public const string Cage = "Cages";
            public const string Worker = "Workers";

            // Основные таблицы (dbo) — 2 таблицы
            public const string Chicken = "Chickens";
            public const string WorkerCageAssignment = "WorkerCageAssignments";
        }
    }
}