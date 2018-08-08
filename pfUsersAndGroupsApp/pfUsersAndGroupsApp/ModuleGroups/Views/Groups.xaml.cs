using System.Windows.Controls;
using ModuleGroups.ViewModel;

namespace ModuleGroups.Views
{
    /// <summary>
    /// Interaction logic for Groups.xaml
    /// </summary>
    public partial class Groups : UserControl
    {
        public Groups()
        {
            InitializeComponent();
            DataContext = new GroupsViewModel();
        }
    }
}
