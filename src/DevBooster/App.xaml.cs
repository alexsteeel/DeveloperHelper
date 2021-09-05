using Microsoft.Extensions.Configuration;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Unity;
using System.Windows;

namespace DevBooster
{
    public partial class App : PrismApplication
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterInstance<IConfiguration>(new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<App>()
                .Build());
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {
            return new DirectoryModuleCatalog() { ModulePath = @".\Modules" };
        }
    }
}
