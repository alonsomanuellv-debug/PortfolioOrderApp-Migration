using System.Collections.Generic;
using PortfolioOrderApp.Models;

namespace PortfolioOrderApp.Services
{
    public class InstrumentCatalogService
    {
        // Simula una tabla de instrumentos en memoria (reemplaza a una base de datos real).
        private static readonly List<Instrument> _instruments = new List<Instrument>
        {
            new Instrument { Id = 1, Ticker = "AAPL", Name = "Apple Inc.", AssetType = "Accion", Price = 195.30m },
            new Instrument { Id = 2, Ticker = "TLT", Name = "iShares 20+ Year Treasury Bond ETF", AssetType = "ETF", Price = 92.15m },
            new Instrument { Id = 3, Ticker = "VWO", Name = "Vanguard FTSE Emerging Markets ETF", AssetType = "ETF", Price = 44.80m },
            new Instrument { Id = 4, Ticker = "BONO10Y", Name = "Bono soberano 10 anios", AssetType = "Bono", Price = 101.20m }
        };

        public List<Instrument> GetInstruments()
        {
            return _instruments;
        }

        public Instrument GetById(int id)
        {
            return _instruments.Find(i => i.Id == id);
        }
    }
}
