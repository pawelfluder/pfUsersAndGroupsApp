﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace EntityFrameworkApp
{
    public class DbManager
    {
        private readonly UserAndGroupsDbEntities _db;

        public DbManager()
        {
            _db = new UserAndGroupsDbEntities();
        }

        //Users
        public List<User> GetUsers()
        {
            List<User> users =_db.Users.ToList();
            return users;
        }

        public void AddUser(string firstName, string lastName)
        {
            _db.Users.Add(new User() { Id = Guid.NewGuid(), FirstName = firstName, LastName = lastName });
            _db.SaveChanges();
        }

        public void RemoveUser(string firstName, string lastName)
        {
            List<User> foundUsers =_db.Users.Where(u => u.FirstName == firstName).ToList();

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
            _db.Groups.Add(new Group() { Id = Guid.NewGuid(), GroupName = groupName});
            _db.SaveChanges();
        }

        public void RemoveGroup(string groupName)
        {
            List<Group> foundGroups = _db.Groups.Where(u => u.GroupName == groupName).ToList();

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

        public void AddAssignment(Guid userId, Guid groupGuid)
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