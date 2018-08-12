using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Data;
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

        public ObservableCollection<UserItem> UserItems { get; set; }

        public ObservableCollection<GroupItem> GroupItems { get; set; }


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
            UserItems = new ObservableCollection<UserItem>();
            GroupItems = new ObservableCollection<GroupItem>();

            //AddAssignmentCommand = new RelayCommand(AddUserMethod);
            //RemoveAssignmentCommand = new RelayCommand(RemoveUserMethod);

            UpdateUserItems();
            UpdateGroupItems();
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
            UserItems.Clear();
            List<User> users = _dbManager.GetUsers();
            foreach (User user in users)
            {
                UserItems.Add(new UserItem(user.FirstName, user.LastName));
            }
        }

        private void UpdateGroupItems()
        {
            GroupItems.Clear();
            List<Group> groups = _dbManager.GetGroups();
            foreach (Group group in groups)
            {
                GroupItems.Add(new GroupItem(@group.GroupName));
            }
        }
    }
}
