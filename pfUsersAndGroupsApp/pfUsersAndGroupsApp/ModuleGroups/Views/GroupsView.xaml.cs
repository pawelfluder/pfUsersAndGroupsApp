using System.Windows.Controls;
using ModuleGroups.ViewModel;

namespace ModuleGroups.Views
{
    public partial class GroupsView : UserControl
    {
        public GroupsView()
        {
            InitializeComponent();
            DataContext = new GroupsViewModel();
        }
    }
}
