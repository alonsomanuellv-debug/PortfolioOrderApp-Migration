Notas para la migracion 

## prerequisitos
## PLANTILLA SELECCIONADA ASP.NET Core MVC
--Se seleccionó la plantilla ASP.NET Core MVC para mantener la arquitectura MVC del proyecto original ya que esto me permite reutilizar gran parte del proyecto original haciendo pequeños ajustes .
##
--se verfica que los paquetes nuget se puedan migrar sin generar errores
--se crea repositorio 
--se vincula con visual studio

## 1-crear proyecto base ASP.NET Core MVC .NET 8 de nombre PortfolioOrderApp.Net8 ya que system.web.mvc no es compatible con net8

## 2- se migran las clases de la carpeta models(OrderLine, Instrument y DepositRequest) del proyecto original:

    -- Eliminación del atributo Serializable ya que net 8 usa un mecanismo de sesiones distinto a net framework (serialización JSON)

## 3- Migración de Servicio Instruments

        -- Reemplazo de System.Web.Mvc por Microsoft.AspNetCore.Mvc. ya que System.Web.Mvc no esta disponible en net8
    
     

## 4- Se crea la clase OrderTicketService en la carpeta services

    -- Se reemplazó el acceso a HttpContext.Current.Session por IHttpContextAccessor ya que ASP.NET Core 8 no admite HttpContext.Current, por lo que fue necesario adaptar el acceso a las sesiones http
    -- Se incorporó System.Text.Json para serializar y deserializar las órdenes almacenadas en sesión.
    -- Se configura Program.cs incorporando AddSession(), AddDistributedMemoryCache() y AddHttpContextAccessor() para habilitar la gestión de sesiones.
    -- Se registra OrderTicketService mediante AddScoped() y se incorpora UseSession() para activar el middleware de sesiones.

## 5- se crea el controller OrderTicketService

    -- Se implementa inyección de dependencias para OrderTicketService, ya que su constructor ahora requiere IHttpContextAccessor.

## 6-Se crea la clase ComplianceAuditLogger en la carpeta Infrastructures

    -- Se reemplaza HttpContext.Current.Server.MapPath() por IWebHostEnvironment.ContentRootPath para obtener la ruta del archivo de auditoría ya que el mecanismo anterior no está disponible en ASP.NET Core 8..
    -- Se adapta ComplianceAuditLogger para utilizar inyección de dependencias en lugar de métodos estáticos.
    -- Se adapta ConfirmTicket() para registrar las operaciones mediante _auditLogger, conservando la lógica original de confirmación y limpieza de sesión.

## 7-Se empieza a trabajar en las solicitudes de deposito

    --Se reemplaza HttpPostedFileBase por IFormFile, debido a que el tipo original no está disponible en ASP.NET Core 8.
    --Se reemplaza HttpContext.Current.Server.MapPath() por IWebHostEnvironment.ContentRootPath y Path.Combine(), debido a la incompatibilidad del mecanismo anterior. 
## 8- Controller deposito

    -- reemplazando System.Web.Mvc por Microsoft.AspNetCore.Mvc y HttpPostedFileBase por IFormFile. Se incorpora inyección de dependencias para DepositRequestService

## 9- Middlewere medicion de requests

    -- Se reemplaza ComplianceTraceModule basado en IHttpModule por ComplianceTraceMiddleware, debido a que System.Web no está disponible en ASP.NET Core 8.
    -- Se conserva Stopwatch para medir la duración de las solicitudes y se reemplaza Trace.WriteLine() por ILogger.LogInformation().