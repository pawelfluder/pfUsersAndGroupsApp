using System;
using System.Collections.Generic;
using System.Linq;

namespace CustomTypesLibrary
{
    public static class GroupUsersItemsOp
    {
        public static List<UserItem> FindNotAssignedUsers(List<UserItem> users, List<AssignmentItem> assignments)
        {
            return users.Where(u => assignments.Select(assignmentItem => assignmentItem.UserId).Any(assignmentItemUserId => assignmentItemUserId == u.Id) == false).ToList();
        }

        public static bool AnyNotAssignedUsers(List<UserItem> users, List<AssignmentItem> assignments)
        {
            return users.Any(u => assignments.Select(assignmentItem => assignmentItem.UserId).Any(assignmentItemUserId => assignmentItemUserId == u.Id) == false);
        }

        public static List<UserItem> GetSampleUsers()
        {
            SampleUsersProvider provider = new SampleUsersProvider();
            Dictionary<string, string> samples = provider.GetSampleUsers();

            List<UserItem> sampleUsers = new List<UserItem>();

            foreach (KeyValuePair<string, string> sampleUser in samples)
            {
                sampleUsers.Add(new UserItem { Id = Guid.NewGuid(), FullName = sampleUser.Key, Email = sampleUser.Value});
            }

            return sampleUsers;
        }
    }
}
