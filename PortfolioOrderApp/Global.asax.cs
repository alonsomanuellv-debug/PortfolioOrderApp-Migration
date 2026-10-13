using System.Web.Mvc;
using System.Web.Routing;

namespace PortfolioOrderApp
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            // Se inicializa la orden en curso (ticket) al arrancar la sesion del usuario.
            Session["CurrentOrderTicket"] = new System.Collections.Generic.List<Models.OrderLine>();
        }
    }
}
