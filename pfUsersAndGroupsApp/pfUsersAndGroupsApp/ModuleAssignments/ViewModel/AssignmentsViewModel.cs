using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using DALEntityframework;

namespace ModuleAssignments.ViewModel
{
    public class AssignmentsViewModel : NotifyBase
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

        public ICommand AddUserCommand { get; set; }

        public ICommand RemoveUserCommand { get; set; }

        public AssignmentsViewModel()
        {
            _dbManager = new DbManager();
            Items = new ObservableCollection<UserItem>();
            AddUserCommand = new RelayCommand(AddUserMethod);
            RemoveUserCommand = new RelayCommand(RemoveUserMethod);

            UpdateUserItems();
        }
        
        private void AddUserMethod(object input)
        {
            _dbManager.AddUser(NewFirstName, NewLastName);
            ClearFormAndUpdateUserItems();
        }

        private void RemoveUserMethod(object input)
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
            Items.Clear();
            List<User> users = _dbManager.GetUsers();
            foreach (User user in users)
            {
                Items.Add(new UserItem(user.FirstName, user.LastName));
            }
        }
    }
}
