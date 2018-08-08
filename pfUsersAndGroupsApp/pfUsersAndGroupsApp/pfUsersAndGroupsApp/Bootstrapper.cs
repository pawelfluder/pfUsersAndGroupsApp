using System.Windows;
using Microsoft.Practices.Unity;
using pfUsersAndGroupsApp.Views;
using Prism.Unity;

namespace pfUsersAndGroupsApp
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
