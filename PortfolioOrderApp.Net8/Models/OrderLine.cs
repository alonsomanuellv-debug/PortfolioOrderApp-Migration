
namespace PortfolioOrderApp.Net8.Models
{
    public enum OrderSide
    {
        Compra,
        Venta
    }

    public class OrderLine
    {
        public int InstrumentId { get; set; }

        public string Ticker { get; set; } 

        public OrderSide Side { get; set; }

        public int Quantity { get; set; }
    }
}
