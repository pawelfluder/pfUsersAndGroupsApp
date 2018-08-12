using System.Windows.Controls;
using ModuleAssignments.ViewModel;

namespace ModuleAssignments.Views
{
    public partial class AssignmentsView : UserControl
    {
        public AssignmentsView()
        {
            InitializeComponent();
            DataContext = new AssignmentsViewModel();
        }
    }
}
