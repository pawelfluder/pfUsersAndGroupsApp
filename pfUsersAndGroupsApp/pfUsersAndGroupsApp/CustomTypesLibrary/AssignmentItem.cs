using System;

namespace CustomTypesLibrary
{
    public struct AssignmentItem
    {
        public Guid Id { get; set; }

        public Guid? GroupId { get; set; }

        public Guid? UserId { get; set; }

        public AssignmentItem(Guid id, Guid? groupId, Guid? userId)
        {
            Id = id;
            GroupId = groupId;
            UserId = userId;
        }
    }
}