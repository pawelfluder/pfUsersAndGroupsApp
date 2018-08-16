using System.Collections.Generic;
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

        public GroupContainer GroupsContainer { get; set; }

        public ObservableCollection<UserItem> UserItems
        {
            get { return new ObservableCollection<UserItem>(GroupsContainer.GroupUsersItems.SelectMany(i => i.UserItems)); }
        }

        public ObservableCollection<GroupItem> GroupItems
        {
            get { return new ObservableCollection<GroupItem>(GroupsContainer.GroupUsersItems.Select(i => i.GroupItem)); }
        }
        
        public UserItem SelectedUser { get; set; }

        public GroupItem SelectedGroup { get; set; }
                
        public ICommand AddAssignmentCommand { get; set; }

        public ICommand RemoveAssignmentCommand { get; set; }

        public AssignmentsViewModel()
        {
            _dbManager = new DbManager();
            GroupsContainer = new GroupContainer();

            AddAssignmentCommand = new RelayCommand(AddAssignmentMethod);
            RemoveAssignmentCommand = new RelayCommand(RemoveAssignmentMethod);

            UpdateGroupContainer();
        }

        private void AddAssignmentMethod(object input)
        {
            _dbManager.AddAssignment(SelectedGroup.Id, SelectedUser.Id);
            UpdateFormAndUpdateGroupContainer();
        }

        private void RemoveAssignmentMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter) input;
            UserItem userToRemove = (UserItem) inputItem.Content;
            _dbManager.RemoveUser(userToRemove.FullName, userToRemove.Email);
            UpdateFormAndUpdateGroupContainer();
        }

        private void UpdateFormAndUpdateGroupContainer()
        {
            UpdateGroupContainer();
            OnPropertyChanged("GroupUsersItems");
        }

        private void UpdateGroupContainer()
        {
            List<UserItem> users = _dbManager.GetUsersWithOutAssigments();
            List<GroupItem> groups = _dbManager.GetGroups();
            List<AssignmentItem> assignments = _dbManager.GetAssigments();

            GroupContainerOp.UpdateGroupContainer(GroupsContainer, assignments, users, groups);
        }
    }
}
