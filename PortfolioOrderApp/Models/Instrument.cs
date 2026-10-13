namespace PortfolioOrderApp.Models
{
    public class Instrument
    {
        public int Id { get; set; }
        public string Ticker { get; set; }
        public string Name { get; set; }
        public string AssetType { get; set; } // "Accion", "Bono", "ETF"
        public decimal Price { get; set; }
    }
}
