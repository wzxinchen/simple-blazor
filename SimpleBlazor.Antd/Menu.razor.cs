using Microsoft.AspNetCore.Components;
using SimpleBlazor.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class Menu
    {
        [Parameter]
        public virtual Action<MenuItem> OnNavigate { get; set; }

        internal void NavigateTo(MenuItem menuItem)
        {
            if (OnNavigate != default)
            {
                OnNavigate(menuItem);
                return;
            }
            Location.Hash = menuItem.Route;
        }
    }
}
