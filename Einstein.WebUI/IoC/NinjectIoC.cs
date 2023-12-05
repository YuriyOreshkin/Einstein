using Einstein.Domain.Services;
using Einstein.WebUI.Controllers.Services;
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
            
           
            ninjectKernel.Bind<ITemplateService>().To<FileTemplateService>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/Templates/OrderTemplate.html")).WithConstructorArgument("_anchor", "order");
            ninjectKernel.Bind<ITemplateService>().To<FileTemplateService>().WhenInjectedInto<MailingTemplateServiceController>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/Templates/MailingTemplate.html")).WithConstructorArgument("_anchor", "mailing");
            ninjectKernel.Bind<ITemplateService>().To<FileTemplateService>().WhenInjectedInto<EMailBackgroundJobs>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/Templates/MailingTemplate.html")).WithConstructorArgument("_anchor", "mailing");
            ninjectKernel.Bind<ITermsService>().To<FileTermsService>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/Terms.html"));

            ninjectKernel.Bind<IXmlService>().To<XMLService>().InSingletonScope().WithConstructorArgument("_path", HttpContext.Current.Server.MapPath("~/App_Data/Settings"));
            ninjectKernel.Bind<IMailServiceConfig>().To<XMLMailServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", "MailSettings.xml");
            ninjectKernel.Bind<IPaymentServiceConfig>().To<XMLPaymentServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", "PaymentSettings.xml");
            ninjectKernel.Bind<IMailingServiceConfig>().To<XMLMailingServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", "MailingSettings.xml");
            ninjectKernel.Bind<ITemplatesServiceConfig>().To<XMLTemplatesServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", "TemplatesSettings.xml");
            ninjectKernel.Bind<IExcel>().To<NPOIExcel>().InSingletonScope().WithConstructorArgument("_template_path", HttpContext.Current.Server.MapPath("~/App_Data/Templates"));

            ninjectKernel.Bind<ISender>().To<EMailSender>().InSingletonScope();
            ninjectKernel.Bind<IOrderSender>().To<EMailOrderSender>().InSingletonScope();
            ninjectKernel.Bind<IBackgroundJobs>().To<EMailBackgroundJobs>().InSingletonScope();
            ninjectKernel.Bind<IUsers>().To<RepositoryUsers>().InSingletonScope();
            ninjectKernel.Bind<ICryptoService>().To<CryptoService>().InSingletonScope();
            return ninjectKernel;
        }
    }
}