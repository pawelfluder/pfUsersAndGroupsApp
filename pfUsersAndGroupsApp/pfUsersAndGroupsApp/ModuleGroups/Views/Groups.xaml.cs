using System.Windows.Controls;
using ModuleGroups.ViewModel;

namespace ModuleGroups.Views
{
    public partial class Groups : UserControl
    {
        public Groups()
        {
            InitializeComponent();
            DataContext = new GroupsViewModel();
        }
    }
}
