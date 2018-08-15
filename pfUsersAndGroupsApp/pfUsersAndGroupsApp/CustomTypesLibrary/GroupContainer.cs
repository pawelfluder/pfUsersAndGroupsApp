using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CustomTypesLibrary
{
    public class GroupContainer : NotifyBase
    {
        public ObservableCollection<GroupUsersItem> GroupUsersItems { get; set; }

        public GroupContainer()
        {
            GroupUsersItems = new ObservableCollection<GroupUsersItem>();
        }

        public void AddUser(GroupItem groupItem, UserItem userItem)
        {
            if (GroupUsersItems.Any(g => g.GroupItem.Id == groupItem.Id))
            {
                GroupUsersItem existingGroup = GroupUsersItems.First(g => g.GroupItem.Id == groupItem.Id);
                if (existingGroup.UserItems.Any(u => u.FullName == userItem.FullName && u.Email == userItem.Email))
                {
                    return;
                }
                existingGroup.AddUserIfNotExists(userItem);
            }
            else
            {
                GroupUsersItems.Add(new GroupUsersItem(groupItem, userItem));
            }
        }

        public void AddUsers(GroupItem groupItem, List<UserItem> userItems)
        {
            foreach (UserItem userItem in userItems)
            {
                AddUser(groupItem, userItem);
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

        public void AddGroups(List<GroupItem> groupItems)
        {
            foreach (GroupItem groupItem in groupItems)
            {
                AddGroup(groupItem);
            }
        }
    }
}
