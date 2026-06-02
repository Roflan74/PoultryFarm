namespace PoultryFarm.Domains
{
    public class Breed
    {
        public long BreedId { get; set; }
        public string Name { get; set; }
        public int AvgEggsPerMonth { get; set; }
        public double AvgWeight { get; set; }
        public long DietId { get; set; } // Связь со справочником диет
    }
}