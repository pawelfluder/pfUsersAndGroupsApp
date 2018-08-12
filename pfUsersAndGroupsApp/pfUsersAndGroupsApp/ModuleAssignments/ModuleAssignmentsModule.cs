using ModuleAssignments.Views;
using Prism.Modularity;
using Prism.Regions;

namespace ModuleAssignments
{
    public class ModuleAssignmentsModule : IModule
    {
        IRegionManager _regionManager;

        public ModuleAssignmentsModule(RegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            _regionManager.RegisterViewWithRegion("ContentRegion", typeof(AssignmentsView));
        }
    }
}