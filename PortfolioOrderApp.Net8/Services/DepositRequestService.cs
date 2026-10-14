using PortfolioOrderApp.Net8.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace PortfolioOrderApp.Net8.Services
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
        private readonly IWebHostEnvironment _environment;

        // Incorporar IWebHostEnvironment mediante inyección de dependencias. 
        public DepositRequestService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public bool TryCreateDeposit(decimal amount, IFormFile receiptFile, out string error)
        {
            error = null;

            var validarext = Path.GetExtension(receiptFile.FileName).ToLowerInvariant(); ;// obtener tipo de archivo ej txt. pdf etc se pasa a minuscula para luego ser validado
           
            //validacion de extencion
            if (validarext != ".jpg" && validarext != ".jpeg" && validarext != ".png" && validarext != ".pdf")
            {
                error = "Solo se permiten archivos JPEG, PNG y PDF.";
                return false;
            }

            const long maxFileSize = 5 * 1024 * 1024;

            if (amount <= 0)
            {
                error = "El monto debe ser mayor a cero.";
                return false;
            }

            if (receiptFile == null || receiptFile.Length == 0)
            {
                error = "Debes adjuntar el comprobante de la transferencia.";
                return false;
            }
            //validacion de 5MB
            if (receiptFile.Length > maxFileSize)
            {
                error = "El comprobante no puede superar los 5 MB.";
                return false;
            }

            var extension = Path.GetExtension(receiptFile.FileName);
            var fileName = string.Format("deposit-{0}{1}", _nextId, extension);

            var uploadsPhysicalPath = Path.Combine(_environment.ContentRootPath, "Content", "uploads", "deposits");
            if (!Directory.Exists(uploadsPhysicalPath))
            {
                Directory.CreateDirectory(uploadsPhysicalPath);
            }

            var physicalFilePath = Path.Combine(uploadsPhysicalPath, fileName);

            using (var stream = new FileStream(physicalFilePath, FileMode.Create))// Si physicalFilePath es un string, debes crear primero un FileStream
            {
                receiptFile.CopyTo(stream);
            }
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

