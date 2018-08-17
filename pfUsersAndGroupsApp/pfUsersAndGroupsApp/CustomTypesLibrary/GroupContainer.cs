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
            AddUnassignedGroupUsersItem();
        }

        private void AddUnassignedGroupUsersItem()
        {
            Guid unassignedGroupId = new Guid();
            GroupItem unassignedGroupItem = new GroupItem(unassignedGroupId, _unassignedGroupName);
            GroupUsersItem unassignedGroupUsersItem = new GroupUsersItem(unassignedGroupItem);
            GroupUsersItems.Add(unassignedGroupUsersItem);
        }

        private bool IsGroupItemCorrect(GroupItem groupItem)
        {
            if (groupItem.Id == Guid.Empty)
            {
                return false;
            }
            if (string.IsNullOrEmpty(groupItem.GroupName))
            {
                return false;
            }

            return true;
        }

        private bool IsUserItemCorrect(UserItem userItem)
        {
            if (userItem.Id == Guid.Empty)
            {
                return false;
            }
            if (string.IsNullOrEmpty(userItem.FullName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(userItem.Email))
            {
                return false;
            }

            return true;
        }

        private bool DoesUserItemExist(UserItem userItem)
        {
            return GroupUsersItems.SelectMany(i => i.UserItems)
                .Any(u => ((u.Id == userItem.Id)) || (u.FullName == userItem.FullName));
        }

        private bool DoesGroupItemExist(GroupItem groupItem)
        {
            return GroupUsersItems.Select(i => i.GroupItem)
                .Any(g => ((g.Id == groupItem.Id)) || (g.GroupName == groupItem.GroupName));
        }

        public void AddUser(GroupItem groupItem, UserItem userItem)
        {
            if (DoesUserItemExist(userItem) == false || IsGroupItemCorrect(groupItem) == false || IsUserItemCorrect(userItem) == false)
            {
                return;
            }

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
            if (GroupUsersItems.SelectMany(i => i.UserItems).Any(u => (u.FullName == userItem.FullName && u.Email == userItem.Email) || (u.Id == userItem.Id)))
            {
                return;
            }

            if (GroupUsersItems.Any(g => g.GroupItem.GroupName == groupName))
            {
                GroupUsersItem existingGroup = GroupUsersItems.First(g => g.GroupItem.GroupName == groupName);
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
                    GroupUsersItems.First(i => i.GroupItem.GroupName == _unassignedGroupName);
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
            if (DoesGroupItemExist(groupItem) == true || IsGroupItemCorrect(groupItem) == false)
            {
                return;
            }

            GroupUsersItems.Add(new GroupUsersItem(groupItem));
        }

        public GroupItem GetUnassignedGroupItem()
        {


            return GroupUsersItems.Select(i => i.GroupItem).First(g => g.GroupName == _unassignedGroupName);
        }

        public void AddGroups(List<GroupItem> groupItems)
        {
            foreach (GroupItem groupItem in groupItems)
            {
                AddGroup(groupItem);
            }
        }

        public void RemoveGroup(GroupItem groupItem)
        {
            if (GroupUsersItems.Select(i => i.GroupItem).Any(u => u.Id == groupItem.Id && u.GroupName == groupItem.GroupName))
            {

                //TOdo check assigments
                //if (IfUserHasAnyAssignment(userToRemove))
                //{
                //    return;
                //}

                GroupUsersItem itemTorRemove = GroupUsersItems.First(i =>
                    i.GroupItem.Id == groupItem.Id && i.GroupItem.GroupName == groupItem.GroupName);

                GroupUsersItems.Remove(itemTorRemove);
            }
        }

        public void AddAssigment(AssignmentItem assignmentItem)
        {
            if (GroupUsersItems.SelectMany(i => i.UserItems).Any(u => u.Id == assignmentItem.UserId))
            {
                UserItem userToAddAssignment = GroupUsersItems.SelectMany(i => i.UserItems)
                    .First(u => u.Id == assignmentItem.UserId);

                GroupItem groupToAddAssignment = GroupUsersItems.Select(i => i.GroupItem)
                    .First(g => g.Id == assignmentItem.GroupId);

                AddUser(groupToAddAssignment, userToAddAssignment);


                //Todo dopisac wszystkim instancja uzytkownika assigments
                //Todo zeminic zeby dodawala sie ta sama referencja przy dodawaniu nowego uzytkownika
                //Todo napisac test sprawdzajacy czy mamay ta sama referencje do uzytkownika np. zmieniajac mu profil i czy zmieni sie wszedzie
                //userToAddAssignment.AddAssignment(assignmentItem);
            }
        }
    }
}
