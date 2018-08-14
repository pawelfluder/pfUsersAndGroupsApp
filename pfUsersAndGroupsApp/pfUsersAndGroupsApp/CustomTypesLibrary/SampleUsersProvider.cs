using System.Collections.Generic;

namespace CustomTypesLibrary
{
    public class SampleUsersProvider
    {
        Dictionary<string, string> _users;

        public Dictionary<string, string> GetSampleUsers()
        {
            return _users;
        }

        public SampleUsersProvider()
        {
            _users = new Dictionary<string, string>();
            _users.Add("Paweł", "Gajęcki");
            _users.Add("Rafał", "Rogodziński");
            _users.Add("Bartosz", "Trębacz");
        }
    }
}
