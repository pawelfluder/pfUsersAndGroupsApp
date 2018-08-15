using System;
using System.Collections.Generic;
using System.Linq;
using CustomTypesLibrary;
using DALEntityframework;
using NUnit.Framework;

namespace pfUsersAndGroupsTests
{
    [TestFixture]
    public class GroupContainerTests
    {
        [Test]
        public void FindNotAssignedUsersTest()
        {
            //Assert
            List<User> sampleUsers = GroupUsersOp.GetSampleUsers();
            List<Assignment> assignments = new List<Assignment>();

            //Act
            List<User> notAssignedUsers = GroupUsersOp.FindNotAssignedUsers(sampleUsers, assignments);

            //Assert
            Assert.AreEqual(sampleUsers.Count, notAssignedUsers.Count);
        }

        [Test]
        public void FindNotAssignedUsersTest2()
        {
            //Assert
            List<User> sampleUsers = GroupUsersOp.GetSampleUsers();
            List<Assignment> assignments = new List<Assignment>();
            assignments.Add(new Assignment(){Id = Guid.NewGuid(), GroupId = Guid.NewGuid(), UserId = sampleUsers.First().Id});

            //Act
            List<User> notAssignedUsers = GroupUsersOp.FindNotAssignedUsers(sampleUsers, assignments);

            //Assert
            Assert.AreEqual(sampleUsers.Count - 1, notAssignedUsers.Count);
        }

        private static GroupContainer GetSampleGroupContainer()
        {
            GroupContainer groupContainer = new GroupContainer();
            SampleUsersProvider provider = new SampleUsersProvider();

            Dictionary<string, string> sampleUsers = provider.GetSampleUsers();
            GroupItem groupItem = new GroupItem(new Guid(), "Unassigned");

            foreach (KeyValuePair<string, string> sampleUser in sampleUsers)
            {
                groupContainer.AddUser(groupItem,
                    new UserItem(Guid.NewGuid(), sampleUser.Key, sampleUser.Value, new List<Guid>()));
            }

            return groupContainer;
        }
    }
}
