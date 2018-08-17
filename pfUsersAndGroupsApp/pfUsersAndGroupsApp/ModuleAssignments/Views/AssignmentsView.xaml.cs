using System.Windows.Controls;
using ViewModelsWpfLibrary;

namespace ModuleAssignments.Views
{
    public partial class AssignmentsView : UserControl
    {
        public AssignmentsView()
        {
            InitializeComponent();
            DataContext = Singleton.GetInstance().GeneralViewModel;
        }
    }
}
