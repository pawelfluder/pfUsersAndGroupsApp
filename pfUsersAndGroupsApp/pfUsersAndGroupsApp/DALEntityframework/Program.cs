using System;
using System.Collections.Generic;

namespace EntityFrameworkApp
{
    class Program
    {
        static void Main()
        {
            DbManager manager = new DbManager();

            List<User> users = manager.GetUsers();
            List<Group> groups = manager.GetGroups();

            manager.RemoveUser("Filip", "Jabloński");
            manager.RemoveUser(new Guid("00000000-0000-0000-0000-000000000000"));


            Console.ReadLine();
        }
    }
}
