using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace CustomTypesLibrary
{
    public class GroupContainer
    {
        public ObservableCollection<GroupUsersItem> GroupUsersItems { get; set; }

        public GroupContainer()
        {
            GroupUsersItems = new ObservableCollection<GroupUsersItem>();
            GroupItem unassignedGroup = new GroupItem(new Guid(), "Unassigned");
            GroupUsersItems.Add(new GroupUsersItem(unassignedGroup));
        }

        public void AddUser(GroupItem groupItem, UserItem userItem)
        {
            if (GroupUsersItems.Any(g => g.GroupItem.Id == groupItem.Id))
            {
                GroupUsersItem existingGroup = GroupUsersItems.First(g => g.GroupItem.Id == groupItem.Id);
                existingGroup.AddUserIfNotExists(userItem);
            }
            else
            {
                GroupUsersItems.Add(new GroupUsersItem(groupItem, userItem));
            }
        }

        public void AddGroup(GroupItem groupItem)
        {
            if (GroupUsersItems.Any(g => g.GroupItem.Id == groupItem.Id))
            {
                return;
            }
            GroupUsersItems.Add(new GroupUsersItem(groupItem));
        }
    }
}
