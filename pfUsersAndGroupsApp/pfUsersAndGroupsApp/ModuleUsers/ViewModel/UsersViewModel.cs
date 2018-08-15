using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using DALEntityframework;

namespace ModuleUsers.ViewModel
{
    public class UsersViewModel : NotifyBase
    {
        private DbManager _dbManager;
        private string _newFullName;
        private string _newEmail;

        public ObservableCollection<UserItem> Items { get; set; }


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

        public UsersViewModel()
        {
            _dbManager = new DbManager();
            Items = new ObservableCollection<UserItem>();
            AddUserCommand = new RelayCommand(AddUserMethod);
            RemoveUserCommand = new RelayCommand(RemoveUserMethod);
            AddSampleUsersCommand = new RelayCommand(AddSampleUsersMethod);

            UpdateUserItems();
        }
        
        private void AddUserMethod(object input)
        {
            _dbManager.AddUser(NewFullName, NewEmail);
            ClearFormAndUpdateUserItems();
        }

        private void RemoveUserMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter) input;
            UserItem userToRemove = (UserItem) inputItem.Content;
            _dbManager.RemoveUser(userToRemove.FullName, userToRemove.Email);
            ClearFormAndUpdateUserItems();
        }

        private void AddSampleUsersMethod(object input)
        {
            SampleUsersProvider provider = new SampleUsersProvider();
            Dictionary<string, string> sampleUsers = provider.GetSampleUsers();
            foreach (KeyValuePair<string, string> sampleUser in sampleUsers)
            {
                _dbManager.AddUser(sampleUser.Key, sampleUser.Value);
            }
            ClearFormAndUpdateUserItems();
        }

        private void ClearFormAndUpdateUserItems()
        {
            NewFullName = string.Empty;
            NewEmail = string.Empty;
            UpdateUserItems();
        }

        private void UpdateUserItems()
        {
            Items.Clear();
            List<User> users = _dbManager.GetUsers();
            foreach (User user in users)
            {
                Items.Add(new UserItem(user.Id, user.FullName, user.Email, new List<Guid>()));
            }
        }
    }
}
