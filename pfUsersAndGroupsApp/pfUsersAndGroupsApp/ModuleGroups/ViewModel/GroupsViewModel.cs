using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleGroups.ViewModel
{
    public class GroupsViewModel
    {
        public ObservableCollection<TodoItem> Items { get; set; }

        public GroupsViewModel()
        {
            Items = new ObservableCollection<TodoItem>();
            Items.Add(new TodoItem("Group 1"));
        }
    }
}
