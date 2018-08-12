using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Data;
using ModuleAssignments.ViewModel;

namespace ModuleAssignments.Views
{
    public partial class AssignmentsView : UserControl
    {
        public AssignmentsView()
        {
            InitializeComponent();

            List<Employee> items = new List<Employee>();
            items.Add(new Employee() { Name = "Deli Dumrul", Department = DeparmentType.Development });
            items.Add(new Employee() { Name = "Ibo Tatlises", Department = DeparmentType.Development });
            items.Add(new Employee() { Name = "Some Dummy", Department = DeparmentType.IT });
            items.Add(new Employee() { Name = "Lorem Traple", Department = DeparmentType.IT });

            EmployeeList.ItemsSource = items;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(EmployeeList.ItemsSource);
            PropertyGroupDescription groupDescription = new PropertyGroupDescription("Department");
            view.GroupDescriptions.Add(groupDescription);
        }

        public enum DeparmentType { Development, IT }

        public class Employee
        {
            public string Name { get; set; }
            public DeparmentType Department { get; set; }
        }
    }
}
