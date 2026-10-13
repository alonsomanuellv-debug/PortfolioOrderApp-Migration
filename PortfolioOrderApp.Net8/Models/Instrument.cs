namespace PortfolioOrderApp.Net8.Models
{
    public class Instrument
    {
        public int Id { get; set; }

        public string Ticker { get; set; } 

        public string Name { get; set; } 

        public string AssetType { get; set; } 

        public decimal Price { get; set; }
    }
}
