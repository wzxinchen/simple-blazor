using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public class LayoutComponentBase : SimpleComponentBase
    {
        internal override string ParentSelector { get; set; } = "body";
    }
}
