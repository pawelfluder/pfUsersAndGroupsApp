using ModuleUsers.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleUsers
{
    public class ModuleUsersModule : IModule
    {
        IRegionManager _regionManager;

        public ModuleUsersModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(UsersView));
        }
    }
}