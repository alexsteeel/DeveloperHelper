using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace ProjectManagement
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

        }
    }
}