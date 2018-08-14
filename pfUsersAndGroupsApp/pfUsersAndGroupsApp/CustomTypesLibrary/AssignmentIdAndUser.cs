using System;

namespace CustomTypesLibrary
{
    public struct AssignmentIdAndUser
    {
        public Guid Id { get; set; }

        public UserItem UserItem { get; set; }

        public AssignmentIdAndUser(Guid id, UserItem userItem)
        {
            Id = id;
            UserItem = userItem;
        }
    }
}
