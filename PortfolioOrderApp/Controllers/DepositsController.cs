using System.Web;
using System.Web.Mvc;
using PortfolioOrderApp.Services;

namespace PortfolioOrderApp.Controllers
{
    public class DepositsController : Controller
    {
        private readonly DepositRequestService _depositService = new DepositRequestService();

        public ActionResult Index()
        {
            var deposits = _depositService.GetAll();
            return View(deposits);
        }

        [HttpPost]
        public ActionResult Create(decimal amount, HttpPostedFileBase receiptFile)
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
