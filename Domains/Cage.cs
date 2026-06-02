namespace PoultryFarm.Domains
{
    public class Cage
    {
        public long CageId { get; set; }
        public int RowNumber { get; set; }
        public int CageNumber { get; set; }
        public long ShopId { get; set; } // Связь со справочником цехов
    }
}