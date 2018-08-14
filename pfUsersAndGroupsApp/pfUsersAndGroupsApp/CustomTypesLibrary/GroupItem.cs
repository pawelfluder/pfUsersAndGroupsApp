using System;

namespace CustomTypesLibrary
{
    public class GroupItem
    {
        public Guid Id { get; set; }

        public string GroupName { get; set; }

        public GroupItem(Guid id, string groupName)
        {
            Id = id;
            GroupName = groupName;
        }
    }
}