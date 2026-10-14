Notas para la migracion 

## prerequisitos
--se verfica que los paquetes nuget se puedan migrar sin generar errores
--se crea repositorio 
--se vincula con visual studio

## 1-crear proyecto base ASP.NET Core MVC .NET 8 de nombre PortfolioOrderApp.Net8 ya que system.web.mvc no es compatible con net8

## 2- se migran las clases de la carpeta models(OrderLine, Instrument y DepositRequest) del proyecto original:

    2.1 Eliminación del atributo Serializable ya que net 8 usa un mecanismo de sesiones distinto a net framework (serialización JSON)

## 3- Migración de Servicio Instruments

    3.1 se crea carpeta services y la clase InstrumentCatalogService (sin cambios)

    3.2 se crea la clase controller InstrumentsController 
        -- Reemplazo de System.Web.Mvc por Microsoft.AspNetCore.Mvc. ya que System.Web.Mvc no esta disponible en net8
    
    3.3 se crea la carpeta View con el index y se pega el codigo sin cambios 

## 4- Se crea la clase OrderTicketService en la carpeta services

    4.1 Se reemplazó el acceso a HttpContext.Current.Session por IHttpContextAccessor ya que ASP.NET Core 8 no admite HttpContext.Current, por lo que fue necesario adaptar el acceso a las sesiones http
    4.2 Se incorporó System.Text.Json para serializar y deserializar las órdenes almacenadas en sesión.
    4.3 Se configura Program.cs incorporando AddSession(), AddDistributedMemoryCache() y AddHttpContextAccessor() para habilitar la gestión de sesiones.
    4.4 Se registra OrderTicketService mediante AddScoped() y se incorpora UseSession() para activar el middleware de sesiones.
## 5. se crea el controller OrderTicketService

    5.1 Se implementa inyección de dependencias para OrderTicketService, ya que su constructor ahora requiere IHttpContextAccessor.