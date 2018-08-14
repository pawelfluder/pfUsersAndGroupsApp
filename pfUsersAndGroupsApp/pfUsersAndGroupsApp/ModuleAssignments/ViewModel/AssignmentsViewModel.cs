﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using DALEntityframework;
using GroupItem = CustomTypesLibrary.GroupItem;

namespace ModuleAssignments.ViewModel
{
    public class AssignmentsViewModel : NotifyBase
    {
        private DbManager _dbManager;
        private string _newFirstName;
        private string _newLastName;

        public ObservableCollection<GroupUsersItem> GroupUsersItems { get; set; }

        public ObservableCollection<UserItem> UserItems { get; set; }

        public ObservableCollection<GroupItem> GroupItems { get; set; }

        public UserItem SelectedUser { get; set; }

        public GroupItem SelectedGroup { get; set; }


        public string NewFirstName
        {
            get { return _newFirstName; }
            set
            {
                _newFirstName = value;
                OnPropertyChanged();
            }
        }

        public string NewLastName
        {
            get { return _newLastName; }
            set
            {
                _newLastName = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddAssignmentCommand { get; set; }

        public ICommand RemoveAssignmentCommand { get; set; }

        public AssignmentsViewModel()
        {
            _dbManager = new DbManager();
            GroupUsersItems = new ObservableCollection<GroupUsersItem>();
            UserItems = new ObservableCollection<UserItem>();
            GroupItems = new ObservableCollection<GroupItem>();

            AddAssignmentCommand = new RelayCommand(AddAssignmentMethod);
            RemoveAssignmentCommand = new RelayCommand(RemoveAssignmentMethod);

            List<Assignment> assignments = _dbManager.GetAssigments();

            List<User> users = _dbManager.GetUsers();
            List<Group> groups = _dbManager.GetGroups();

            foreach (Assignment assignment in assignments)
            {
                User user = users.First(u => u.Id == assignment.UserId);
                UserItem userItem = new UserItem(user.Id, user.FirstName, user.LastName);

                Group group = groups.First(u => u.Id == assignment.GroupId);
                GroupItem groupItem = new GroupItem(group.Id, group.GroupName);

                GroupUsersItem existingGroup = null;
                if (GroupUsersItems.Any())
                {
                    existingGroup = GroupUsersItems.First(ug => ug.GroupItem.Id == assignment.GroupId);
                }

                if (existingGroup == null)
                {
                    GroupUsersItems.Add(new GroupUsersItem(groupItem, assignment.Id, userItem));
                }
                else
                {
                    existingGroup.AddUserIfNotExists(assignment.Id, userItem);
                }


            }


            UpdateUserItems();
            UpdateGroupItems();
        }
        
        private void AddAssignmentMethod(object input)
        {
            _dbManager.AddAssignment(SelectedUser.Id, SelectedGroup.Id);
            ClearFormAndUpdateUserItems();
        }

        private void RemoveAssignmentMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter) input;
            UserItem userToRemove = (UserItem) inputItem.Content;
            _dbManager.RemoveUser(userToRemove.FirstName, userToRemove.LastName);
            ClearFormAndUpdateUserItems();
        }

        private void ClearFormAndUpdateUserItems()
        {
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            UpdateUserItems();
        }

        private void UpdateUserItems()
        {
            UserItems.Clear();
            List<User> users = _dbManager.GetUsers();
            foreach (User user in users)
            {
                UserItems.Add(new UserItem(user.Id, user.FirstName, user.LastName));
            }
        }

        private void UpdateGroupItems()
        {
            GroupItems.Clear();
            List<Group> groups = _dbManager.GetGroups();
            foreach (Group group in groups)
            {
                GroupItems.Add(new GroupItem(group.Id, group.GroupName));
            }
        }
    }
}
