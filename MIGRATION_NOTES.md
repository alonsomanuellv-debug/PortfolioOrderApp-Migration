Notas para la migracion 

## prerequisitos
--se verfica que los paquetes nuget se puedan migrar sin generar errores
--se crea repositorio 
--se vincula con visual studio

## 1-crear proyecto base ASP.NET Core MVC .NET 8 de nombre PortfolioOrderApp.Net8 ya que system.web.mvc no es compatible con net8

## 2- se migran las clases de la carpeta models(OrderLine, Instrument y DepositRequest) del proyecto original:

    2.1 Eliminación del atributo Serializable ya que net 8 usa un mecanismo de sesiones distinto a net framework

## 3- Migración de Servicio Instruments

    3.1 se crea carpeta services y la clase InstrumentCatalogService (sin cambios)

    3.2 se crea la clase controller InstrumentsController 
        -- Reemplazo de System.Web.Mvc por Microsoft.AspNetCore.Mvc. ya que System.Web.Mvc no esta disponible en net8
    
    3.3 se crea la carpeta View con el index y se pega el codigo sin cambios 