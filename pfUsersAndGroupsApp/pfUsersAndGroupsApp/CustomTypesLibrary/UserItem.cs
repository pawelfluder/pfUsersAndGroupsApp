namespace CustomTypesLibrary
{
    public class UserItem
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public UserItem(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}