using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using GroupItem = System.Windows.Controls.GroupItem;

namespace ViewModelsWpfLibrary.ViewModels
{
    public partial class GeneralViewModel : NotifyBase
    {
        private string _newFullName;
        private string _newEmail;


        public string NewFullName
        {
            get { return _newFullName; }
            set
            {
                _newFullName = value;
                OnPropertyChanged();
            }
        }

        public string NewEmail
        {
            get { return _newEmail; }
            set
            {
                _newEmail = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddUserCommand { get; set; }

        public ICommand RemoveUserCommand { get; set; }

        public ICommand AddSampleUsersCommand { get; set; }

        private void UsersCtor()
        {
            AddUserCommand = new RelayCommand(AddUserMethod);
            RemoveUserCommand = new RelayCommand(RemoveUserMethod);
            AddSampleUsersCommand = new RelayCommand(AddSampleUsersMethod);
        }

        private void AddUserMethod(object input)
        {
            Guid userId = Guid.NewGuid();
            UserItem userItem = new UserItem(userId, NewFullName, NewEmail);

            _dbManager.AddUser(userId, NewFullName, NewEmail);
            GroupsContainer.AddUnassignedUser(userItem);

            ClearUsersForm();
            UpdateUsersView();
        }

        private void RemoveUserMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter)input;
            UserItem userToRemove = (UserItem)inputItem.Content;

            bool ifUserHasAnyAssignment = GroupsContainer.IfUserHasAnyAssignment(userToRemove);

            if (ifUserHasAnyAssignment == false)
            {
                _dbManager.RemoveUser(userToRemove.FullName, userToRemove.Email);
                GroupsContainer.RemoveUser(userToRemove.FullName, userToRemove.Email);

                UpdateUsersView();
                ClearUsersForm();
            }
        }

        private void AddSampleUsersMethod(object input)
        {
            SampleUsersProvider provider = new SampleUsersProvider();
            Dictionary<string, string> sampleUsers = provider.GetSampleUsers();
            foreach (KeyValuePair<string, string> sampleUser in sampleUsers)
            {
                Guid userId = Guid.NewGuid();
                _dbManager.AddUser(userId, sampleUser.Key, sampleUser.Value);
                GroupsContainer.AddUnassignedUser(sampleUser.Key, sampleUser.Value);
            }

            UpdateUsersView();
            ClearUsersForm();
        }

        private void ClearUsersForm()
        {
            NewFullName = string.Empty;
            NewEmail = string.Empty;
        }

        private void UpdateUsersView()
        {
            OnPropertyChanged("UserItems");
            OnPropertyChanged("GroupUsersItems");
        }
    }
}
