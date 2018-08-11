using System.Windows.Controls;
using ModuleUsers.ViewModel;

namespace ModuleUsers.Views
{
    public partial class UsersView : UserControl
    {
        public UsersView()
        {
            InitializeComponent();
            DataContext = new UsersViewModel();
        }
    }
}
