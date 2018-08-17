using System;
using System.Collections.Generic;
using System.Linq;
using CustomTypesLibrary;

namespace DALEntityframework
{
    public class DbManager
    {
        private readonly UserAndGroupsDbEntities _db;

        public DbManager()
        {
            _db = new UserAndGroupsDbEntities();
        }

        //Users
        public List<UserItem> GetUsersWithOutAssigments()
        {
            if (_db.Users.Any())
            {
                List<User> users = _db.Users.ToList();
                List<UserItem> userItems = users.Select(u => new UserItem(u.Id, u.FullName, u.Email)).ToList();
                return userItems;
            }
            return new List<UserItem>();
        }

        public List<UserItem> GetUsersWithAssigments()
        {
            List<UserItem> userItems = GetUsersWithOutAssigments();
            List<AssignmentItem> assignmentItems = _db.Assignments.Select(u => new AssignmentItem(u.Id, u.GroupId, u.UserId)).ToList();

            AddAssignmentsToUsers(userItems, assignmentItems);
            return userItems;
        }

        private void AddAssignmentsToUsers(List<UserItem> userItems, List<AssignmentItem> userIdAndAssignId)
        {
            foreach (UserItem userItem in userItems)
            {
                userItem.AddAssignments(userIdAndAssignId.Where(a => a.UserId == userItem.Id).ToList());
            }
        }

        public void AddUser(Guid id, string FullName, string Email)
        {
            bool userExistsInDb = _db.Users.Any(u => u.FullName == FullName && u.Email == Email);

            if (userExistsInDb == false)
            {
                _db.Users.Add(new User() { Id = id, FullName = FullName, Email = Email });
                _db.SaveChanges();
            }
        }

        public void RemoveUser(string fullName, string email)
        {
            if (_db.Users.Any(u => u.FullName == fullName && u.Email == email))
            {
                List<User> foundUsers = _db.Users.Where(u => u.FullName == fullName && u.Email == email).ToList();
                _db.Users.RemoveRange(foundUsers);
                _db.SaveChanges();
            }
        }

        public void RemoveUser(Guid userId)
        {
            if (_db.Users.Any(u => u.Id == userId))
            {
                List<User> foundUsers = _db.Users.Where(u => u.Id == userId).ToList();
                _db.Users.RemoveRange(foundUsers);
                _db.SaveChanges();
            }
        }

        //Groups
        public List<GroupItem> GetGroups()
        {
            if (_db.Groups.Any())
            {
                List<Group> groups = _db.Groups.ToList();
                List<GroupItem> groupItems = groups.Select(g => new GroupItem(g.Id, g.GroupName)).ToList();
                return groupItems;
            }
            return new List<GroupItem>();
        }

        public void AddGroup(GroupItem groupItem)
        {
            bool groupExistsInDb = _db.Groups.Any(u => u.GroupName == groupItem.GroupName);

            if (groupExistsInDb == false)
            {
                _db.Groups.Add(new Group() {Id = groupItem.Id, GroupName = groupItem.GroupName});
                _db.SaveChanges();
            }
        }

        public void RemoveGroup(string groupName)
        {
            List<Group> groups = _db.Groups.ToList();

            var foundGroups = _db.Groups.Where(u => u.GroupName == groupName).ToList();

            if (foundGroups.Any())
            {
                _db.Groups.RemoveRange(foundGroups);
                _db.SaveChanges();
            }
        }

        public void RemoveGroup(Guid id)
        {
            List<Group> foundGroups = _db.Groups.Where(u => u.Id == id).ToList();

            if (foundGroups != null)
            {
                _db.Groups.RemoveRange(foundGroups);
                _db.SaveChanges();
            }
        }

        //Assigments
        public List<AssignmentItem> GetAssigments()
        {
            if (_db.Groups.Any())
            {
                List<Assignment> assignments = _db.Assignments.ToList();
                List<AssignmentItem> assignmentItems = assignments.Select(g => new AssignmentItem(g.Id, g.GroupId, g.UserId)).ToList();
                return assignmentItems;
            }
            return new List<AssignmentItem>();
        }

        public void AddAssignment(AssignmentItem assignmentItem)
        {
            List<User> foundUserIds = _db.Users.Where(u => u.Id == assignmentItem.UserId).ToList();
            List<Group> foundGoupIds = _db.Groups.Where(g => g.Id == assignmentItem.GroupId).ToList();

            if (foundUserIds.Any() && foundGoupIds.Any())
            {
                _db.Assignments.Add(new Assignment() { Id = assignmentItem.Id, UserId = assignmentItem.UserId, GroupId = assignmentItem.GroupId });
                _db.SaveChanges();
            }
        }

        public void RemoveAssignments(Guid userId, Guid groupId)
        {
            List<Assignment> foundAssignments = _db.Assignments.Where(a => a.UserId == userId && a.GroupId == groupId).ToList();

            if (foundAssignments.Any())
            {
                _db.Assignments.RemoveRange(foundAssignments);
                _db.SaveChanges();
            }
        }

        public void RemoveAssigments(Guid id)
        {
            List<Group> foundGroups = _db.Groups.Where(u => u.Id == id).ToList();

            if (foundGroups.Any())
            {
                _db.Groups.RemoveRange(foundGroups);
                _db.SaveChanges();
            }
        }
    }
}