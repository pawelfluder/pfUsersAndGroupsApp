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
        public List<UserItem> GetUsersWitOutAssigments()
        {
            //Dictionary<Guid, Guid> assignmentIds = 
                var gg = _db.Assignments.Select(u => new KeyValuePair<Guid, Guid>(u.Id, u.UserId));
            List<UserItem> userItems = _db.Users.Select(u => new UserItem(u.Id, u.FullName, u.FullName, new List<Guid>())).ToList();
            AddAssignmentsToUsers(userItems, assignmentIds);
            return userItems;
        }

        public List<UserItem> AddAssignmentsToUsers(List<UserItem> userItems, List<string> assignmentIds)
        {

        }

        public List<UserItem> GetUsersWitAssigments()
        {
            List<UserItem> userItems = _db.Users.Select(u => new UserItem(u.Id, u.FullName, u.FullName, new List<Guid>())).ToList();
            return userItems;
        }

        public void AddUser(string FullName, string Email)
        {
            bool userExistsInDb = _db.Users.Any(u => u.FullName == FullName && u.Email == Email);

            if (userExistsInDb == false)
            {
                _db.Users.Add(new User() { Id = Guid.NewGuid(), FullName = FullName, Email = Email });
                _db.SaveChanges();
            }
        }

        public void RemoveUser(string FullName, string Email)
        {
            List<User> foundUsers =_db.Users.Where(u => u.FullName == FullName && u.Email == Email).ToList();

            if (foundUsers.Any())
            {
                _db.Users.RemoveRange(foundUsers);
                _db.SaveChanges();
            }
        }

        public void RemoveUser(Guid id)
        {
            List<User> foundUsers = _db.Users.Where(u => u.Id == id).ToList();

            if (foundUsers.Any())
            {
                _db.Users.RemoveRange(foundUsers);
                _db.SaveChanges();
            }
        }

        //Groups
        public List<Group> GetGroups()
        {
            List<Group> groups = _db.Groups.ToList();
            return groups;
        }

        public void AddGroup(string groupName)
        {
            bool groupExistsInDb = _db.Groups.Any(u => u.GroupName == groupName);

            if (groupExistsInDb == false)
            {
                _db.Groups.Add(new Group() {Id = Guid.NewGuid(), GroupName = groupName});
                _db.SaveChanges();
            }
        }

        public void RemoveGroup(string groupName)
        {
            List<Group> groups = _db.Groups.ToList();

            var foundGroups = _db.Groups.Where(u => u.GroupName == groupName).ToList();

            foreach (Group foundGroup in foundGroups)
            {
                
            }

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
        public List<Assignment> GetAssigments()
        {
            List<Assignment> groups = _db.Assignments.ToList();
            return groups;
        }

        public void AddAssignment(Guid groupGuid, Guid userId)
        {
            List<User> foundUserIds = _db.Users.Where(u => u.Id == userId).ToList();
            List<Group> foundGoupIds = _db.Groups.Where(g => g.Id == groupGuid).ToList();

            if (foundUserIds.Any() && foundGoupIds.Any())
            {
                _db.Assignments.Add(new Assignment() { Id = Guid.NewGuid(), UserId = userId, GroupId = groupGuid });
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