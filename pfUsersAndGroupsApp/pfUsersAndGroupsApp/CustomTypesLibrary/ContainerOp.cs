using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomTypesLibrary
{
    public static class ContainerOp
    {
        public static void UpdateGroupContainer(GroupContainer groupsContainer, List<Assignment> assignments, List<User> users, List<Group> groups)
        {
            bool existsNotAssignedUsers = GroupUsersOp.AnyNotAssignedUsers(users, assignments);

            if (existsNotAssignedUsers)
            {
                List<User> notAssignedUsers = GroupUsersOp.FindNotAssignedUsers(users, assignments);
                List<UserItem> notAssignedUserItems =
                    notAssignedUsers.Select(u => new UserItem(u.Id, u.FullName, u.Email, new List<Guid>())).ToList();
                GroupItem groupItem = new GroupItem(new Guid(), "Unassigned");
                groupsContainer.AddUsers(groupItem, notAssignedUserItems);

                users = users.Except(notAssignedUsers).ToList();
            }

            foreach (Assignment assignment in assignments)
            {
                User user = users.First(u => u.Id == assignment.UserId);
                UserItem userItem = new UserItem(user.Id, user.FullName, user.Email, assignment.Id);

                Group group = groups.First(u => u.Id == assignment.GroupId);
                GroupItem groupItem = new GroupItem(group.Id, group.GroupName);

                groupsContainer.AddUser(groupItem, userItem);
            }

            List<GroupItem> groupItems =
                groups.Select(u => new GroupItem(u.Id, u.GroupName)).ToList();
            groupsContainer.AddGroups(groupItems);
        }
    }
}
