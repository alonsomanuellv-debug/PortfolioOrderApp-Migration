using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using PortfolioOrderApp.Models;

namespace PortfolioOrderApp.Services
{
    public class DepositRequestService
    {
        private const string UploadsVirtualPath = "~/Content/uploads/deposits";

        private static readonly List<DepositRequest> _deposits = new List<DepositRequest>();
        private static int _nextId = 1;

        public List<DepositRequest> GetAll()
        {
            return _deposits.OrderByDescending(d => d.RequestedAtUtc).ToList();
        }


        public bool TryCreateDeposit(decimal amount, HttpPostedFileBase receiptFile, out string error)
        {
            error = null;

            if (amount <= 0)
            {
                error = "El monto debe ser mayor a cero.";
                return false;
            }

            if (receiptFile == null || receiptFile.ContentLength == 0)
            {
                error = "Debes adjuntar el comprobante de la transferencia.";
                return false;
            }


            var extension = Path.GetExtension(receiptFile.FileName);
            var fileName = string.Format("deposit-{0}{1}", _nextId, extension);

            var uploadsPhysicalPath = HttpContext.Current.Server.MapPath(UploadsVirtualPath);
            if (!Directory.Exists(uploadsPhysicalPath))
            {
                Directory.CreateDirectory(uploadsPhysicalPath);
            }

            var physicalFilePath = Path.Combine(uploadsPhysicalPath, fileName);
            receiptFile.SaveAs(physicalFilePath);

            _deposits.Add(new DepositRequest
            {
                Id = _nextId,
                Amount = amount,
                ReceiptFileName = fileName,
                RequestedAtUtc = DateTime.UtcNow,
                Status = "Pendiente de validacion"
            });

            _nextId++;
            return true;
        }
    }
}
