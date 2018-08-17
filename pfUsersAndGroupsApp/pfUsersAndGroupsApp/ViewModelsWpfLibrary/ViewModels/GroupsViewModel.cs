 using System;
 using System.Collections.Generic;
 using System.Windows.Controls;
 using System.Windows.Input;
 using CustomTypesLibrary;
 using GroupItem = CustomTypesLibrary.GroupItem;

namespace ViewModelsWpfLibrary.ViewModels
{
    public partial class GeneralViewModel : NotifyBase
    {
        private string _newGroupName;

        public string NewGroupName
        {
            get { return _newGroupName; }
            set
            {
                _newGroupName = value;
                OnPropertyChanged();
            }
        }

        public ICommand AddGroupCommand { get; set; }

        public ICommand RemoveGroupCommand { get; set; }

        public ICommand AddSampleGroupsCommand { get; set; }

        public void GroupsCtor()
        {
            AddGroupCommand = new RelayCommand(AddGroupMethod);
            RemoveGroupCommand = new RelayCommand(RemoveGroupMethod);
            AddSampleGroupsCommand = new RelayCommand(AddSampleGroupsMethod);
        }

        private void AddGroupMethod(object input)
        {
            Guid groupId = Guid.NewGuid();
            GroupItem groupItem = new GroupItem(groupId, NewGroupName);

            _dbManager.AddGroup(groupItem);
            GroupsContainer.AddGroup(groupItem);
            
            UpdateGroupsView();
        }

        private void RemoveGroupMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter)input;
            GroupItem groupToRemove = (GroupItem)inputItem.Content;

            _dbManager.RemoveGroup(groupToRemove.GroupName);
            GroupsContainer.RemoveGroup(groupToRemove);

            UpdateGroupsView();
        }

        private void AddSampleGroupsMethod(object input)
        {
            SampleGroupsProvider provider = new SampleGroupsProvider();
            List<string> sampleGrops = provider.GetSampleGroups();
            foreach (string sampleGroup in sampleGrops)
            {
                Guid groupId = Guid.NewGuid();
                GroupItem groupItem = new GroupItem(groupId, sampleGroup);

                GroupsContainer.AddGroup(groupItem);
                _dbManager.AddGroup(groupItem);
            }
            UpdateGroupsView();
        }

        private void UpdateGroupsView()
        {
            OnPropertyChanged("GroupUsersItems");
            OnPropertyChanged("GroupItems");
        }
    }
}
