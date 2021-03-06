﻿using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CustomTypesLibrary;
using DALEntityframework;

namespace ViewModelsWpfLibrary.ViewModels
{
    public partial class GeneralViewModel : NotifyBase
    {
        private DbManager _dbManager;

        public GroupContainer GroupsContainer { get; set; }

        public ObservableCollection<UserItem> UserItems
        {
            get { return new ObservableCollection<UserItem>(GroupsContainer.GroupUsersItems.SelectMany(i => i.UserItems)); }
        }

        public ObservableCollection<GroupItem> GroupItems
        {
            get
            {
                IEnumerable<GroupItem> groupItem = new List<GroupItem>() {GroupsContainer.GetUnassignedGroupItem()};
                return new ObservableCollection<GroupItem>(GroupsContainer.GroupUsersItems.Select(i => i.GroupItem).Except(groupItem));
            }
        }

        public GeneralViewModel()
        {
            _dbManager = new DbManager();
            GroupsContainer = new GroupContainer();
            UpdateGroupContainer();
            OnPropertyChanged("GroupUsersItems");
            OnPropertyChanged("UserItems");
            OnPropertyChanged("GroupItems");

            UsersCtor();
            GroupsCtor();
            AssignmentsCtor();
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
