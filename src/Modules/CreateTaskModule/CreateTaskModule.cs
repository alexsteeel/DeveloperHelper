using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CreateTask
{
    public class CreateTaskModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(CreateTaskView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}