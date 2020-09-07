using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoggingTest
{
    public class UserServiceInterceptor : IInterceptor
    {
        public virtual void Intercept(IInvocation invocation)
        {
            Console.WriteLine($"{DateTime.Now}: Before method execution");
            invocation.Proceed();
            Console.WriteLine($"{DateTime.Now}: After method execution");
        }
    }
}
