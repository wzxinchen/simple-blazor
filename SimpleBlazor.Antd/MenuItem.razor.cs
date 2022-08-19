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
        public bool IsSelected { get; set; }
        [Parameter]
        public string Route { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public object Owner { get; set; }

        private void NavigateTo()
        {
            if(Owner is Menu menu)
            {
                menu.NavigateTo(this);
            }
        }
    }
}
