using Microsoft.AspNetCore.Components;
using SimpleBlazor.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class TabPanel : IOwnerBy
    {
        [Parameter]
        public virtual object Owner { get; set; }

        [Parameter]
        public virtual bool IsActive { get; set; }

        protected override void OnParametersSet()
        {
            if (Owner is Tab tab)
            {
                tab.RegisterTabPanel(this);
            }
        }
    }
}
