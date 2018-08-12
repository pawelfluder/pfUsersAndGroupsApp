using System.Windows;
using Microsoft.Practices.Unity;
using ModuleAssignments.Views;
using ModuleGroups.Views;
using ModuleUsers.Views;
using Prism.Regions;

namespace pfUsersAndGroupsApp.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        IUnityContainer _container;
        IRegionManager _regionManager;
        IRegion _region;

        UsersView _usersView;
        GroupsView _groupsView;
        AssignmentsView _assignmentsView;

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _usersView = _container.Resolve<UsersView>();
            _groupsView = _container.Resolve<GroupsView>();
            _assignmentsView = _container.Resolve<AssignmentsView>();

            _region = _regionManager.Regions["ContentRegion"];

            _region.Add(_usersView);
            _region.Add(_groupsView);
            _region.Add(_assignmentsView);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _region.Deactivate(_groupsView);
            _region.Activate(_usersView);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _region.Deactivate(_usersView);
            _region.Activate(_groupsView);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            _region.Deactivate(_assignmentsView);
            _region.Activate(_assignmentsView);
        }
    }
}
