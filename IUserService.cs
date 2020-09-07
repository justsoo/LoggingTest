using Autofac.Extras.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest
{
    [Intercept("log-calls")]
    public interface IUserService
    {
        string GetUserName();
    }
}
