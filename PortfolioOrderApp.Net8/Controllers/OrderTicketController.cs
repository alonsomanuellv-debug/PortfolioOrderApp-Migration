using Microsoft.AspNetCore.Mvc;
using PortfolioOrderApp.Net8.Models;
using PortfolioOrderApp.Net8.Services;

namespace PortfolioOrderApp.Net8.Controllers
{
    public class OrderTicketController : Controller
    {
        private readonly OrderTicketService _ticketService;

        private readonly InstrumentCatalogService _catalogService = new InstrumentCatalogService();

        public OrderTicketController(OrderTicketService ticketService)
        {
            _ticketService = ticketService;
        }
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
