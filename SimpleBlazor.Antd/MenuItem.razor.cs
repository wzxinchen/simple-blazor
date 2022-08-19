using Microsoft.AspNetCore.Components;
using SimpleBlazor.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class MenuItem : IOwnerBy
    {
        [Parameter]
        public virtual bool IsSelected { get; set; }
        [Parameter]
        public virtual string Route { get; set; }

        [Parameter]
        public virtual object Owner { get; set; }

        private void NavigateTo()
        {
            if(Owner is Menu menu)
            {
                menu.NavigateTo(this);
            }
        }
    }
}
