using System.Collections.Generic;
using System.Collections.ObjectModel;
using CustomTypesLibrary;
using EntityFrameworkApp;

namespace ModuleUsers.ViewModel
{
    public class GroupsViewModel
    {
        public ObservableCollection<UserItem> Items { get; set; }

        public GroupsViewModel()
        {
            Items = new ObservableCollection<UserItem>();
            
            DbManager dbManager = new DbManager();
            List<User> users = dbManager.GetUsers();

            foreach (User user in users)
            {
                Items.Add(new UserItem(user.FirstName, user.LastName));
            }
        }
    }
}
