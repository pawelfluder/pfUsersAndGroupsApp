using System;
using System.Collections.Generic;

namespace CustomTypesLibrary
{
    public struct UserItem
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string Email { get; set; }

        public string Profile => FullName + " " + Email;

        public List<AssignmentItem> AssignmentIds { get; set; }

        public UserItem(Guid id, string fullName, string email, List<AssignmentItem> assignmentIds)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            AssignmentIds = assignmentIds;
        }

        public UserItem(Guid id, string fullName, string email)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            AssignmentIds = new List<AssignmentItem>();
        }

        public UserItem(Guid id, string fullName, string email, AssignmentItem assignmentItem)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            AssignmentIds = new List<AssignmentItem>
            {
                assignmentItem
            };
        }

        public void AddAssignmentId(AssignmentItem assignmentItem)
        {
            AssignmentIds.Add(assignmentItem);
        }

        public void AddAssignmentIds(List<AssignmentItem> assignmentItems)
        {
            AssignmentIds.AddRange(assignmentItems);
        }
    }
}