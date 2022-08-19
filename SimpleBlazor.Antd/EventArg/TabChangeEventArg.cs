using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd.EventArg
{
    public class TabChangeEventArg<T>
    {
        public T Before { get; }
        public T Current { get; }
    }
}
