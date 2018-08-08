using System.Collections.ObjectModel;
using CustomTypesLibrary;

namespace ModuleUsers.ViewModel
{
    public class GroupsViewModel
    {
        public ObservableCollection<TodoItem> Items { get; set; }

        public GroupsViewModel()
        {
            Items = new ObservableCollection<TodoItem>();
            Items.Add(new TodoItem("User 1"));
            Items.Add(new TodoItem("User 2"));
            Items.Add(new TodoItem("User 3"));
            Items.Add(new TodoItem("User 4"));
        }
    }
}
