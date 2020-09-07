using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest
{
    public class UserService : IUserService
    {
        public string GetUserName()
        {
            return "oec2003";
        }
    }
}
