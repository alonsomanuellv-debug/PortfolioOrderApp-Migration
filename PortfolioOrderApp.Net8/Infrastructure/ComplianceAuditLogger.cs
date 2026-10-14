
using Microsoft.AspNetCore.Hosting;
using PortfolioOrderApp.Net8.Models;
using System;
using System.IO;

namespace PortfolioOrderApp.Net8.Infrastructure
{
    public class ComplianceAuditLogger
    {
        private readonly string _logPath;

        public ComplianceAuditLogger(IWebHostEnvironment environment)
        {
            var directory = Path.Combine(
                environment.ContentRootPath,
                "App_Data");

            Directory.CreateDirectory(directory);

            _logPath = Path.Combine(
                directory,
                "compliance_audit.log");
        }

        public void LogOrderConfirmation(
            string ticker, OrderSide side, int quantity)
        {
            var line = string.Format(
                "{0:o} - Orden confirmada: {1} {2} x{3}{4}",
                DateTime.UtcNow,
                side,
                ticker,
                quantity,
                Environment.NewLine);

            File.AppendAllText(_logPath, line);
        }
    }
}
