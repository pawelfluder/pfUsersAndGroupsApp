using System.Windows;
using Microsoft.Practices.Unity;
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

        UsersView _viewA;
        GroupsView _viewB;

        public MainWindow(IUnityContainer container, IRegionManager regionManager)
        {
            InitializeComponent();
            _container = container;
            _regionManager = regionManager;

            this.Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            _viewA = _container.Resolve<UsersView>();
            _viewB = _container.Resolve<GroupsView>();

            _region = _regionManager.Regions["ContentRegion"];

            _region.Add(_viewA);
            _region.Add(_viewB);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            _region.Deactivate(_viewB);
            _region.Activate(_viewA);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            _region.Deactivate(_viewA);
            _region.Activate(_viewB);
        }
    }
}
