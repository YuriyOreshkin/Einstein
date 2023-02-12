using Einstein.WebUI.IoC;
using System.Web.Mvc;
using System.Web.Http;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web;
using Einstein.Domain.Services;
using Einstein.WebUI.Models;
using Ninject;

namespace Einstein.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            // dependency injection
            var kernel = NinjectIoC.Initialize();
            // Web Api
            GlobalConfiguration.Configuration.DependencyResolver = new Ninject.Web.WebApi.NinjectDependencyResolver(kernel);
            // MVC 
            System.Web.Mvc.DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));

            //ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory());
        }

        protected void Application_PostAuthenticateRequest()
        {

            if (Request.IsAuthenticated)
            {

                var user = HttpContext.Current.User.Identity;

                IRepository repos = NinjectIoC.Initialize().Get<IRepository>();

                CustomPrincipal customPrincipal = new CustomPrincipal(user, repos);

                HttpContext.Current.User = customPrincipal;




            }
        }
    }
}
