using Ninject;
using Ninject.Web.Common;
using Ninject.Extensions.Conventions;
using R3MUS.Devpack.SSO.IntelMap.Database;
using R3MUS.Devpack.SSO.IntelMap.Helpers;
using R3MUS.Devpack.SSO.IntelMap.Models;

namespace R3MUS.Devpack.SSO.IntelMap.App_Start
{
    public partial class Startup
    {
        private static void ConfigureBindings(IKernel kernel)
        {
            kernel.Bind<DatabaseContext>().ToSelf().InRequestScope();

            kernel.Bind(x =>
            {
                x.FromThisAssembly()
                    .SelectAllClasses()
                    .Excluding<DatabaseContext>()
                    .Excluding<DummyUserStore<SSOApplicationUser>>()
                    .BindDefaultInterface()
                    .Configure(y => y.InRequestScope());
            });
        }
    }
}