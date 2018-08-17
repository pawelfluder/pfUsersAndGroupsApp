using System.Windows.Controls;
using ViewModelsWpfLibrary;

namespace ModuleGroups.Views
{
    public partial class GroupsView : UserControl
    {
        public GroupsView()
        {
            InitializeComponent();
            DataContext = Singleton.GetInstance().GeneralViewModel;
        }
    }
}
