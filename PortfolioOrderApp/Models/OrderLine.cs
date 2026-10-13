using System;

namespace PortfolioOrderApp.Models
{
    public enum OrderSide
    {
        Compra,
        Venta
    }

    [Serializable]
    public class OrderLine
    {
        public int InstrumentId { get; set; }
        public string Ticker { get; set; }
        public OrderSide Side { get; set; }
        public int Quantity { get; set; }
    }
}
