using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace Reflection
{
    public class ReflectionModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
            var regionManager = containerProvider.Resolve<IRegionManager>();
            regionManager.RegisterViewWithRegion("ContentRegion", typeof(TypesView));
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}