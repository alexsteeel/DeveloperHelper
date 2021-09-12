using Microsoft.Extensions.Logging;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ProjectManagementModule
{
    public class ProjectManagementModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("CreateProjectViewRegion", typeof(CreateProjectView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<ILogger, TextLogger>();
            containerRegistry.Register<IDotnetClient, DotnetClient>();
        }
    }
}