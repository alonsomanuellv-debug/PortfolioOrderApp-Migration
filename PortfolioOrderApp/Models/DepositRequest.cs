using System;

namespace PortfolioOrderApp.Models
{
    public class DepositRequest
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public string ReceiptFileName { get; set; }
        public DateTime RequestedAtUtc { get; set; }
        public string Status { get; set; }
    }
}
