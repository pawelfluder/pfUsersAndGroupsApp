using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityFrameworkApp
{
    class ExampleUsers
    {
        ExampleUsers()
        {
            Dictionary<string,string> users = new Dictionary<string, string>();
            users.Add("Filip", "Jabloński");
            users.Add("Michał", "Kowalski");
        }
    }
}
