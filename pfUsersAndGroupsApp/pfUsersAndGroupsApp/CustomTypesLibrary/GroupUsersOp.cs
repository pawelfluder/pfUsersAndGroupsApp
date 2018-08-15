using System;
using System.Collections.Generic;
using System.Linq;
using DALEntityframework;

namespace CustomTypesLibrary
{
    public static class GroupUsersOp
    {
        public static List<User> FindNotAssignedUsers(List<User> users, List<Assignment> assignments)
        {
            return users.Where(u => assignments.Select(a => a.UserId).Any(id => id == u.Id) == false).ToList();
        }

        public static bool AnyNotAssignedUsers(List<User> users, List<Assignment> assignments)
        {
            return users.Any(u => assignments.Select(a => a.UserId).Any(id => id == u.Id) == false);
        }

        public static List<User> GetSampleUsers()
        {
            SampleUsersProvider provider = new SampleUsersProvider();
            Dictionary<string, string> samples = provider.GetSampleUsers();

            List<User> sampleUsers = new List<User>();

            foreach (KeyValuePair<string, string> sampleUser in samples)
            {
                sampleUsers.Add(new User{Id = Guid.NewGuid(), FullName = sampleUser.Key, Email = sampleUser.Value});
            }

            return sampleUsers;
        }
    }
}
