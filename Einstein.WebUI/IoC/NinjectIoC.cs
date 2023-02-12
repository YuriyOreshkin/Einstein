using Einstein.Domain.Services;
using Ninject;
using Ninject.Web.WebApi.Filter;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace Einstein.WebUI.IoC
{
    public static class NinjectIoC
    {
        public static IKernel Initialize()
        {
            IKernel kernel = new StandardKernel();
            
            AddBindings(kernel);
            return kernel;
        }

        private static IKernel AddBindings(IKernel ninjectKernel)
        {
            ninjectKernel.Bind<DefaultFilterProviders>().ToConstant(new DefaultFilterProviders(new[] { new NinjectFilterProvider(ninjectKernel) }.AsEnumerable()));
            ninjectKernel.Bind<DefaultModelValidatorProviders>().ToConstant(new DefaultModelValidatorProviders(GlobalConfiguration.Configuration.Services.GetModelValidatorProviders()));

            ninjectKernel.Bind<IRepository>().To<EFRepository>();
            ninjectKernel.Bind<IMailServiceConfig>().To<XMLMailServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/MailSettings.xml"));
            ninjectKernel.Bind<ITemplateService>().To<FileTemplateService>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/OrderTemplate.html"));
            ninjectKernel.Bind<ITermsService>().To<FileTermsService>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/Terms.html"));
            ninjectKernel.Bind<IPaymentServiceConfig>().To<XMLPaymentServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/PaymentSettings.xml"));
            
            ninjectKernel.Bind<IMailSender>().To<EMailSender>().InSingletonScope();
            ninjectKernel.Bind<IUsers>().To<RepositoryUsers>().InSingletonScope();
            ninjectKernel.Bind<ICryptoService>().To<CryptoService>().InSingletonScope();
            return ninjectKernel;
        }
    }
}