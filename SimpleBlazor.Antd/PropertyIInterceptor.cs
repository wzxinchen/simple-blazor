using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public class PropertyInterceptor : IInterceptor
    {
        public bool StateHasChanged { get; private set; }
        public void Intercept(IInvocation invocation)
        {
            invocation.Proceed();
            if (invocation.Method.Name.StartsWith("get_"))
            {
                return;
            }
            StateHasChanged = true;
        }
    }
}
