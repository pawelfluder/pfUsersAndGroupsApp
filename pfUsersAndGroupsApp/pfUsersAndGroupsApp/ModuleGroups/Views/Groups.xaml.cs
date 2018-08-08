using System.Collections.Generic;
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
            //DataContext = new GroupsViewModel();

            List<TodoItem> items = new List<TodoItem>();
            items.Add(new TodoItem("Complete this WPF tutorial"));
            items.Add(new TodoItem("Learn C#"));
            items.Add(new TodoItem("Wash the car"));

            icTodoList.ItemsSource = items;
        }
    }
}
