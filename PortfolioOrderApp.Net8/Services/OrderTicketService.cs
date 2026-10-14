
using Microsoft.AspNetCore.Http;
using PortfolioOrderApp.Net8.Infrastructure;
using PortfolioOrderApp.Net8.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace PortfolioOrderApp.Net8.Services
{
    public class OrderTicketService
    {
        private const string SessionKey = "CurrentOrderTicket";

        private readonly IHttpContextAccessor _httpContextAccessor;

        private readonly ComplianceAuditLogger _auditLogger;

        private static readonly List<OrderLine> _confirmedOrders =
            new List<OrderLine>();

        // Constructor con inyección de dependencias con audit logger
        public OrderTicketService( IHttpContextAccessor httpContextAccessor,ComplianceAuditLogger auditLogger)
        {
            _httpContextAccessor = httpContextAccessor;
            _auditLogger = auditLogger;
        }

        // Obtener sesión actual
        private ISession GetSession()
        {
            return _httpContextAccessor.HttpContext?.Session
                ?? throw new InvalidOperationException(
                    "No hay una sesión HTTP disponible.");
        }

        // Obtener órdenes temporales
        public List<OrderLine> GetDraft()
        {
            var session = GetSession();

            var json = session.GetString(SessionKey);

            if (string.IsNullOrEmpty(json))
            {
                var draft = new List<OrderLine>();

                session.SetString(
                    SessionKey,
                    JsonSerializer.Serialize(draft));

                return draft;
            }

            return JsonSerializer.Deserialize<List<OrderLine>>(json)
                ?? new List<OrderLine>();
        }

        // Agregar una orden
        public void AddLine(OrderLine line)
        {
            var draft = GetDraft();

            draft.Add(line);

            GetSession().SetString(
                SessionKey,
                JsonSerializer.Serialize(draft));
        }

        // Calcular total estimado
        public decimal GetEstimatedTotal(IEnumerable<Instrument> catalog)
        {
            var draft = GetDraft();

            return draft.Sum(l =>
            {
                var instrument = catalog.FirstOrDefault(
                    i => i.Id == l.InstrumentId);

                return instrument == null
                    ? 0
                    : l.Quantity * instrument.Price;
            });
        }

      //  Confirmar órdenes
        public void ConfirmTicket(IEnumerable<Instrument> catalog)
        {
            var draft = GetDraft();

            foreach (var line in draft)
            {
                _confirmedOrders.Add(line);

                var instrument = catalog.FirstOrDefault(
                    i => i.Id == line.InstrumentId);

                     _auditLogger.LogOrderConfirmation(
                    instrument != null ? instrument.Ticker : "N/A",
                    line.Side,
                    line.Quantity);
            }

            // Limpiar órdenes temporales
            GetSession().SetString(
                SessionKey,
                JsonSerializer.Serialize(new List<OrderLine>()));
        }
    }
}
