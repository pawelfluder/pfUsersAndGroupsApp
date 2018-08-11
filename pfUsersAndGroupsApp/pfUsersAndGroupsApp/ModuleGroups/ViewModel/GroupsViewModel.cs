using System.Collections.Generic;
using System.Collections.ObjectModel;
using CustomTypesLibrary;
using EntityFrameworkApp;

namespace ModuleGroups.ViewModel
{
    public class GroupsViewModel
    {
        public ObservableCollection<GroupItem> Items { get; set; }

        public GroupsViewModel()
        {
            Items = new ObservableCollection<GroupItem>();
            
            DbManager dbManager = new DbManager();
            List<Group> groups = dbManager.GetGroups();

            foreach (Group group in groups)
            {
                Items.Add(new GroupItem(group.GroupName));
            }
        }
    }
}
