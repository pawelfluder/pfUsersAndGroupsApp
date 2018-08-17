using System.Windows.Controls;
using ViewModelsWpfLibrary;

namespace ModuleUsers.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            DataContext = Singleton.GetInstance().GeneralViewModel;
        }
    }
}
