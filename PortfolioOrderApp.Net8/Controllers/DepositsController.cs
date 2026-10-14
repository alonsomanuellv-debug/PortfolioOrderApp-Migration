
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PortfolioOrderApp.Net8.Services;

namespace PortfolioOrderApp.Net8.Controllers
{
    public class DepositsController : Controller
    {
        private readonly DepositRequestService _depositService;

        public DepositsController(DepositRequestService depositService)
        {
            _depositService = depositService;
        }

        public ActionResult Index()
        {
            var deposits = _depositService.GetAll();
            return View(deposits);
        }

        [HttpPost]
        public ActionResult Create(decimal amount, IFormFile receiptFile)
        {
            string error;

            if (!_depositService.TryCreateDeposit(amount, receiptFile, out error))
            {
                ModelState.AddModelError("receiptFile", error);
                return View("Index", _depositService.GetAll());
            }

            return RedirectToAction("Index");
        }
    }
}
