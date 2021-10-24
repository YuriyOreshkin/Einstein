using Einstein.WebUI.IoC;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace Einstein.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // dependency injection
            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }
    }
}
