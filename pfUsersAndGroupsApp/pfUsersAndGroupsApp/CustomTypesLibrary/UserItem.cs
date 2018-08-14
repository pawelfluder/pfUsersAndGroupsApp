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

        public List<Guid> AssignmentIds { get; set; }

        public UserItem(Guid id, string fullName, string email, List<Guid> assignmentIds)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            AssignmentIds = assignmentIds;
        }

        public UserItem(Guid id, string fullName, string email, Guid assignmentId)
        {
            Id = id;
            FullName = fullName;
            Email = email;
            AssignmentIds = new List<Guid>
            {
                assignmentId
            };
        }
    }
}