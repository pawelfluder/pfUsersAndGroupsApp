using System.Collections.Generic;

namespace CustomTypesLibrary
{
    public class SampleGroupsProvider
    {
        List<string> _users;

        public List<string> GetSampleGroups()
        {
            return _users;
        }

        public SampleGroupsProvider()
        {
            _users = new List<string>();
            _users.Add("Grupa strzelców");
            _users.Add("Grupa ochotnicza");
            _users.Add("Grupa wsparcia");
            _users.Add("Grupa rolników");
        }
    }
}
