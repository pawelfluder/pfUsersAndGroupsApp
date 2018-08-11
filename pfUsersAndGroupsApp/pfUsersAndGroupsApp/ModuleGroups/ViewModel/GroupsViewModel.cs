using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using CustomTypesLibrary;
using EntityFrameworkApp;

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

        public GroupsViewModel()
        {
            _dbManager = new DbManager();
            Items = new ObservableCollection<GroupItem>();
            AddGroupCommand = new RelayCommand(AddGroupMethod);
            
            UpdateGroupItems();
        }

        private void UpdateGroupItems()
        {
            Items.Clear();
            List<Group> groups = _dbManager.GetGroups();
            foreach (Group group in groups)
            {
                Items.Add(new GroupItem(@group.GroupName));
            }
        }

        private void AddGroupMethod(object input)
        {
            _dbManager.AddGroup(NewGroupName);
            NewGroupName = string.Empty;
            UpdateGroupItems();
        }
    }
}
