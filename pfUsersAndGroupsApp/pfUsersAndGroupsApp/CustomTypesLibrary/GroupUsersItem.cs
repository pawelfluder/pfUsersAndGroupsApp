using System.Collections.ObjectModel;
using System.Linq;

namespace CustomTypesLibrary
{
    public class GroupUsersItem
    {
        //Todo remove setters
        public GroupItem GroupItem { get; }
        public ObservableCollection<UserItem> UserItems { get; set; }

        public GroupUsersItem(GroupItem groupItem, UserItem userItem)
        {
            GroupItem = groupItem;
            UserItems = new ObservableCollection<UserItem>();
            UserItems.Add(userItem);
        }

        public GroupUsersItem(GroupItem groupItem)
        {
            GroupItem = groupItem;
            UserItems = new ObservableCollection<UserItem>();
        }

        public void AddUserIfNotExists(UserItem userItem)
        {
            if (UserItems.Any(u => u.Id == userItem.Id) || UserItems.Any(u => u.FirstName == userItem.FirstName && u.LastName == userItem.LastName))
            {
                return;
            }
            UserItems.Add(userItem);
        }
    }
}