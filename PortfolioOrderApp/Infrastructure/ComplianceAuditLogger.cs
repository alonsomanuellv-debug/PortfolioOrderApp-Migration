using System;
using System.IO;
using System.Web;
using PortfolioOrderApp.Models;

namespace PortfolioOrderApp.Infrastructure
{
    public static class ComplianceAuditLogger
    {
        public static void LogOrderConfirmation(string ticker, OrderSide side, int quantity)
        {
            var logPath = HttpContext.Current.Server.MapPath("~/App_Data/compliance_audit.log");
            var line = string.Format(
                "{0:o} - Orden confirmada: {1} {2} x{3}{4}",
                DateTime.UtcNow, side, ticker, quantity, Environment.NewLine);

            File.AppendAllText(logPath, line);
        }
    }
}
