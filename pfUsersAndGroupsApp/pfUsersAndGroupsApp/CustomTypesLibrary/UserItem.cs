using System;
using System.Collections.Generic;

namespace CustomTypesLibrary
{
    public struct UserItem
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName => FirstName + " " + LastName;

        public List<Guid> AssignmentIds { get; set; }

        public UserItem(Guid id, string firstName, string lastName, List<Guid> assignmentIds)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AssignmentIds = assignmentIds;
        }

        public UserItem(Guid id, string firstName, string lastName, Guid assignmentId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AssignmentIds = new List<Guid>
            {
                assignmentId
            };
        }
    }
}