using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortfolioOrderApp.Infrastructure;
using PortfolioOrderApp.Models;

namespace PortfolioOrderApp.Services
{

    public class OrderTicketService
    {
        private const string SessionKey = "CurrentOrderTicket";

        private static readonly List<OrderLine> _confirmedOrders = new List<OrderLine>();

        public List<OrderLine> GetDraft()
        {
            var draft = HttpContext.Current.Session[SessionKey] as List<OrderLine>;
            if (draft == null)
            {
                draft = new List<OrderLine>();
                HttpContext.Current.Session[SessionKey] = draft;
            }
            return draft;
        }

        public void AddLine(OrderLine line)
        {
            var draft = GetDraft();
            draft.Add(line);
            HttpContext.Current.Session[SessionKey] = draft;
        }

        public decimal GetEstimatedTotal(IEnumerable<Instrument> catalog)
        {
            var draft = GetDraft();
            return draft.Sum(l =>
            {
                var instrument = catalog.FirstOrDefault(i => i.Id == l.InstrumentId);
                return instrument == null ? 0 : l.Quantity * instrument.Price;
            });
        }

        public void ConfirmTicket(IEnumerable<Instrument> catalog)
        {
            var draft = GetDraft();

            foreach (var line in draft)
            {
                _confirmedOrders.Add(line);
                var instrument = catalog.FirstOrDefault(i => i.Id == line.InstrumentId);
                ComplianceAuditLogger.LogOrderConfirmation(
                    instrument != null ? instrument.Ticker : "N/A",
                    line.Side,
                    line.Quantity);
            }

            HttpContext.Current.Session[SessionKey] = new List<OrderLine>();
        }
    }
}
