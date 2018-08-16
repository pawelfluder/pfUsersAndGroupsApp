using System.Windows.Controls;
using ViewModelsWpfLibrary.ViewModels;

namespace ModuleUsers.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            DataContext = new GeneralViewModel();
        }
    }
}
