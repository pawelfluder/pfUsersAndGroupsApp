using System.Windows.Controls;
using ViewModelsWpfLibrary.ViewModels;

namespace ModuleAssignments.Views
{
    public partial class AssignmentsView : UserControl
    {
        public AssignmentsView()
        {
            InitializeComponent();
            DataContext = new GeneralViewModel();
        }
    }
}
