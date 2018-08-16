using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CustomTypesLibrary
{
    public class GroupContainer : NotifyBase
    {
        public ObservableCollection<GroupUsersItem> GroupUsersItems { get; set; }

        private string _unassignedGroupName = "Unassigned";

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

        public void AddUnassignedUser(string fullName, string email)
        {
            UserItem userItem = new UserItem(Guid.NewGuid(), fullName, email);
            AddUnassignedUser(userItem);
        }

        public void AddUnassignedUser(UserItem userItem)
        {
            AddUser(_unassignedGroupName, userItem);
        }

        public void AddUser(string groupName, UserItem userItem)
        {
            if (GroupUsersItems.Any(g => g.GroupItem.GroupName == groupName))
            {
                GroupUsersItem existingGroup = GroupUsersItems.First(g => g.GroupItem.GroupName == groupName);
                if (existingGroup.UserItems.Any(u => u.FullName == userItem.FullName && u.Email == userItem.Email))
                {
                    return;
                }

                existingGroup.AddUserIfNotExists(userItem);
            }
            else
            {
                GroupItem groupItem = new GroupItem(Guid.NewGuid(), groupName);
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

        public void RemoveUser(string fullName, string email)
        {
            if (GroupUsersItems.SelectMany(i => i.UserItems).Any(u => u.FullName == fullName && u.Email == email))
            {
                UserItem userToRemove = GroupUsersItems.SelectMany(i => i.UserItems)
                    .First(u => u.FullName == fullName && u.Email == email);

                if (IfUserHasAnyAssignment(userToRemove))
                {
                    return;
                }

                GroupUsersItem unassignedGroupUsersItem =
                    GroupUsersItems.First(i => i.GroupItem.GroupName == "Unassigned");
                unassignedGroupUsersItem.UserItems.Remove(userToRemove);
            }
        }

        public bool IfUserHasAnyAssignment(UserItem user)
        {
            return GroupUsersItems.SelectMany(i => i.UserItems).SelectMany(i => i.AssignmentIds)
                .Any(i => i.UserId == user.Id);
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

        public void AddAssigment(AssignmentItem assignmentItem)
        {
            if (GroupUsersItems.SelectMany(i => i.UserItems).Any(u => u.Id == assignmentItem.UserId))
            {
                UserItem userToAddAssignment = GroupUsersItems.SelectMany(i => i.UserItems)
                    .First(u => u.Id == assignmentItem.UserId);

                userToAddAssignment.AddAssignment(assignmentItem);
            }
        }
    }
}
