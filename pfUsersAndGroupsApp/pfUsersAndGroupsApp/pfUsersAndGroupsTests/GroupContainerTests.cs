using System;
using System.Collections.Generic;
using System.Linq;
using CustomTypesLibrary;
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
            List<UserItem> sampleUsers = GroupUsersItemsOp.GetSampleUsers();
            List<AssignmentItem> assignments = new List<AssignmentItem>();

            //Act
            List<UserItem> notAssignedUsers = GroupUsersItemsOp.FindNotAssignedUsers(sampleUsers, assignments);

            //Assert
            Assert.AreEqual(sampleUsers.Count, notAssignedUsers.Count);
        }

        [Test]
        public void FindNotAssignedUsersTest2()
        {
            //Assert
            List<UserItem> sampleUsers = GroupUsersItemsOp.GetSampleUsers();
            List<AssignmentItem> assignments = new List<AssignmentItem>();
            assignments.Add(new AssignmentItem(){Id = Guid.NewGuid(), GroupId = Guid.NewGuid(), UserId = sampleUsers.First().Id});

            //Act
            List<UserItem> notAssignedUsers = GroupUsersItemsOp.FindNotAssignedUsers(sampleUsers, assignments);

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
                    new UserItem(Guid.NewGuid(), sampleUser.Key, sampleUser.Value, new List<AssignmentItem>()));
            }

            return groupContainer;
        }
    }
}
