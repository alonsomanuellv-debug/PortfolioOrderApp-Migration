
using Microsoft.AspNetCore.Mvc;
using PortfolioOrderApp.Net8.Services;

namespace PortfolioOrderApp.Net8.Controllers
{
    public class InstrumentsController : Controller
    {
        private readonly InstrumentCatalogService _catalogService = new InstrumentCatalogService();

        public ActionResult Index()
        {
            var instruments = _catalogService.GetInstruments();

            return View(instruments);
        }
    }
}
