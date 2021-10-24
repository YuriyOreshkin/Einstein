using Einstein.Domain.Services;
using Ninject;

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
           
            return ninjectKernel;
        }
    }
}