using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomTypesLibrary
{
    public class GroupUsersItem
    {
        public GroupItem GroupItem { get; set; }
        public List<AssignmentIdAndUser> IdAndUser { get; set; }


        public GroupUsersItem(GroupItem groupItem, List<AssignmentIdAndUser> idAndUser)
        {
            GroupItem = groupItem;
            IdAndUser = idAndUser;
        }

        public GroupUsersItem(GroupItem groupItem, Guid id, UserItem userItem )
        {
            GroupItem = groupItem;
            IdAndUser = new List<AssignmentIdAndUser>();
            IdAndUser.Add(new AssignmentIdAndUser(id, userItem));
        }

        public void AddUserIfNotExists(Guid id, UserItem userItem)
        {
            if (IdAndUser.Any(iau => iau.Id == id) || IdAndUser.Any(iau => iau.UserItem.FirstName == userItem.FirstName && iau.UserItem.LastName == userItem.LastName))
            {
                return;
            }
            IdAndUser.Add(new AssignmentIdAndUser(id, userItem));
        }
    }
}