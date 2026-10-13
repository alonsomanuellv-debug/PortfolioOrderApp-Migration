using System.Web.Mvc;
using PortfolioOrderApp.Services;

namespace PortfolioOrderApp.Controllers
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
