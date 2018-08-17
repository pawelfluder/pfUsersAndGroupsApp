using System;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using DALEntityframework;
using GroupItem = CustomTypesLibrary.GroupItem;

namespace ViewModelsWpfLibrary.ViewModels
{
    public partial class GeneralViewModel : NotifyBase
    {
        public UserItem SelectedUser { get; set; }

        public GroupItem SelectedGroup { get; set; }
        
        public ICommand AddAssignmentCommand { get; set; }

        public ICommand RemoveAssignmentCommand { get; set; }

        private void AssignmentsCtor()
        {
            AddAssignmentCommand = new RelayCommand(AddAssignmentMethod);
            RemoveAssignmentCommand = new RelayCommand(RemoveAssignmentMethod);
        }

        private void AddAssignmentMethod(object input)
        {
            Guid assignmentId = Guid.NewGuid();
            AssignmentItem assignmentItem = new AssignmentItem(assignmentId, SelectedGroup.Id, SelectedUser.Id);

            _dbManager.AddAssignment(assignmentItem);
            GroupsContainer.AddAssigment(assignmentItem);

            UpdateAssignmentsView();
        }

        private void RemoveAssignmentMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter)input;
            UserItem userToRemove = (UserItem)inputItem.Content;
            _dbManager.RemoveUser(userToRemove.FullName, userToRemove.Email);


            UpdateAssignmentsView();
        }

        private void UpdateAssignmentsView()
        {
            OnPropertyChanged("UserItems");
            OnPropertyChanged("GroupItems");
            OnPropertyChanged("GroupUsersItems");
        }
    }
}
