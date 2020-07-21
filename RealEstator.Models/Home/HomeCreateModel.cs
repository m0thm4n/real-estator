namespace RealEstator.Models.Home
{
    public class HomeCreateModel
    {
        public int HomeID { get; set; }
        public string Address { get; set; } = string.Empty;
        public int Beds { get; set; }
        public int Baths { get; set; }
        public int SquareFootage { get; set; }
        public bool HasPool { get; set; }
        public bool IsWaterfront { get; set; }
        public bool Occupied { get; set; }
        public int YearBuilt { get; set; }
        public int Price { get; set; }
    }
}