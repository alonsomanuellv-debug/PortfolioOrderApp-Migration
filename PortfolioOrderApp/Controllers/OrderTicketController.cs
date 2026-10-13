using System.Web.Mvc;
using PortfolioOrderApp.Models;
using PortfolioOrderApp.Services;

namespace PortfolioOrderApp.Controllers
{
    public class OrderTicketController : Controller
    {
        private readonly OrderTicketService _ticketService = new OrderTicketService();
        private readonly InstrumentCatalogService _catalogService = new InstrumentCatalogService();

        public ActionResult Index()
        {
            var draft = _ticketService.GetDraft();
            ViewBag.EstimatedTotal = _ticketService.GetEstimatedTotal(_catalogService.GetInstruments());
            return View(draft);
        }

        [HttpPost]
        public ActionResult Add(int instrumentId, OrderSide side, int quantity, string ticker)
        {
            _ticketService.AddLine(new OrderLine { InstrumentId = instrumentId, Side = side, Quantity = quantity, Ticker = ticker });
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Confirm()
        {
            _ticketService.ConfirmTicket(_catalogService.GetInstruments());
            return RedirectToAction("Index");
        }
    }
}
