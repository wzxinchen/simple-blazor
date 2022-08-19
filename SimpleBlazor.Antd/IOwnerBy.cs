using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    /// <summary>
    /// 一个组件会调用其他组件，比如，Layout.razor 组件调用 Menu 组件，那么 Menu 组件若实现这个接口，它将可以拿到 Layout 组件
    /// </summary>
    public interface IOwnerBy
    {
        object Owner { get; set; }
    }
}
