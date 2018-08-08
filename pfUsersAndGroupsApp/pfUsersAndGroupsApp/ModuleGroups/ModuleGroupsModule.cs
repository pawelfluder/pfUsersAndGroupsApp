using ModuleGroups.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleGroups
{
    public class ModuleGroupsModule : IModule
    {
        IRegionManager _regionManager;

        public ModuleGroupsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(Groups));
        }
    }
}