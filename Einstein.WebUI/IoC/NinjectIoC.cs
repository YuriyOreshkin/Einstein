using Einstein.Domain.Services;
using Ninject;
using System.Web;

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
            ninjectKernel.Bind<IRepository>().To<EFRepository>();
            ninjectKernel.Bind<IMailServiceConfig>().To<XMLMailServiceConfig>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/MailSettings.xml"));
            ninjectKernel.Bind<ITemplateService>().To<FileTemplateService>().InSingletonScope().WithConstructorArgument("_filename", HttpContext.Current.Server.MapPath("~/App_Data/OrderTemplate.html"));
            ninjectKernel.Bind<IMailSender>().To<EMailSender>().InSingletonScope();
            ninjectKernel.Bind<IUsers>().To<SettingsUsers>().InSingletonScope();
            return ninjectKernel;
        }
    }
}