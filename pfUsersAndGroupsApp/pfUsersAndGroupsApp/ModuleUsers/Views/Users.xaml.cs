using System.Windows.Controls;
using ModuleUsers.ViewModel;

namespace ModuleUsers.Views
{
    public partial class Users : UserControl
    {
        public Users()
        {
            InitializeComponent();
            DataContext = new GroupsViewModel();
        }
    }
}
