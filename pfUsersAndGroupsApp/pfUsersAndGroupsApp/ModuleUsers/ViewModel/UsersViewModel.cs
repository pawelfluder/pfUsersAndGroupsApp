using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CustomTypesLibrary;
using EntityFrameworkApp;

namespace ModuleUsers.ViewModel
{
    public class UsersViewModel : NotifyBase
    {
        private DbManager _dbManager;
        private string _newFirstName;
        private string _newLastName;

        public ObservableCollection<UserItem> Items { get; set; }


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

        public ICommand AddUserCommand
        {
            get;
            set;
        }

        public UsersViewModel()
        {
            _dbManager = new DbManager();
            Items = new ObservableCollection<UserItem>();
            AddUserCommand = new RelayCommand(AddUserMethod);

            UpdateUserItems();
        }

        private void UpdateUserItems()
        {
            Items.Clear();
            List<User> users = _dbManager.GetUsers();
            foreach (User user in users)
            {
                Items.Add(new UserItem(user.FirstName, user.LastName));
            }
        }

        private void AddUserMethod(object input)
        {
            _dbManager.AddUser(NewFirstName, NewLastName);
            NewFirstName = string.Empty;
            NewLastName = string.Empty;
            UpdateUserItems();
        }
    }
}
