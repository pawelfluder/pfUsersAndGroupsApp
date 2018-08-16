using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomTypesLibrary
{
    public static class GroupContainerOp
    {
        public static void UpdateGroupContainer(GroupContainer groupsContainer, List<AssignmentItem> assignmentItems, List<UserItem> userItems, List<GroupItem> groupItems)
        {
            bool existsNotAssignedUsers = GroupUsersItemsOp.AnyNotAssignedUsers(userItems, assignmentItems);

            if (existsNotAssignedUsers)
            {
                List<UserItem> notAssignedUserItems = GroupUsersItemsOp.FindNotAssignedUsers(userItems, assignmentItems);

                GroupItem groupItem = new GroupItem(new Guid(), "Unassigned");
                groupsContainer.AddUsers(groupItem, notAssignedUserItems);

                userItems = userItems.Except(notAssignedUserItems).ToList();
            }

            foreach (AssignmentItem assignment in assignmentItems)
            {
                UserItem usersFromAssignment = userItems.First(u => u.Id == assignment.UserId);

                GroupItem groupFromAssignment = groupItems.First(u => u.Id == assignment.GroupId);

                groupsContainer.AddUser(groupFromAssignment, usersFromAssignment);
            }

            groupsContainer.AddGroups(groupItems);
        }

        public static void UpdateGroupContainer(GroupContainer groupsContainer, List<UserItem> userItems, List<GroupItem> groupItems)
        {
            bool existsNotAssignedUsers = userItems.Any(u => u.AssignmentIds.Any() == false);

            if (existsNotAssignedUsers)
            {
                List<UserItem> notAssignedUserItems = userItems.Where(u => u.AssignmentIds.Any() == false).ToList();

                GroupItem groupItem = new GroupItem(new Guid(), "Unassigned");
                groupsContainer.AddUsers(groupItem, notAssignedUserItems);

                userItems = userItems.Except(notAssignedUserItems).ToList();
            }

            foreach (GroupItem groupItem in groupItems)
            {
                UserItem usersFromGroup = userItems.First(u => u.AssignmentIds.Any(a => a.GroupId == groupItem.Id));

                groupsContainer.AddUser(groupItem, usersFromGroup);
            }

            groupsContainer.AddGroups(groupItems);
        }
    }
}
