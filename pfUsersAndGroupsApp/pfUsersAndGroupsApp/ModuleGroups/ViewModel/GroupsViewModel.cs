using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;
using CustomTypesLibrary;
using DALEntityframework;
using GroupItem = CustomTypesLibrary.GroupItem;

namespace ModuleGroups.ViewModel
{
    public class GroupsViewModel : NotifyBase
    {
        private readonly DbManager _dbManager;
        private string _newGroupName;
        private ObservableCollection<GroupItem> _items;

        public ObservableCollection<GroupItem> Items
        {
            get { return _items; }
            set
            {
                _items = value;
                OnPropertyChanged();
            }
        }

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

        public GroupsViewModel()
        {
            _dbManager = new DbManager();
            Items = new ObservableCollection<GroupItem>();
            AddGroupCommand = new RelayCommand(AddGroupMethod);
            RemoveGroupCommand = new RelayCommand(RemoveGroupMethod);
            AddSampleGroupsCommand = new RelayCommand(AddSampleGroupsMethod);

            UpdateGroupItems();
        }

        private void AddGroupMethod(object input)
        {
            _dbManager.AddGroup(NewGroupName);
            ClearFormAndUpdateUserItems();
        }

        private void RemoveGroupMethod(object input)
        {
            ContentPresenter inputItem = (ContentPresenter)input;
            GroupItem groupToRemove = (GroupItem)inputItem.Content;
            _dbManager.RemoveGroup(groupToRemove.GroupName);
            ClearFormAndUpdateUserItems();
        }

        private void AddSampleGroupsMethod(object input)
        {
            SampleGroupsProvider provider = new SampleGroupsProvider();
            List<string> sampleGrops = provider.GetSampleGroups();
            foreach (string sampleGroup in sampleGrops)
            {
                _dbManager.AddGroup(sampleGroup);
            }
            ClearFormAndUpdateUserItems();
        }

        private void ClearFormAndUpdateUserItems()
        {
            NewGroupName = string.Empty;
            UpdateGroupItems();
        }

        private void UpdateGroupItems()
        {
            Items.Clear();
            List<GroupItem> groups = _dbManager.GetGroups();
            Items.AddRange(groups);
        }
    }
}
