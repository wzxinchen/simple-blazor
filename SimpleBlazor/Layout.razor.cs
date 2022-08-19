using Microsoft.AspNetCore.Components;
using SimpleBlazor.Antd;
using SimpleBlazor.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor
{
    public partial class Layout
    {
        [Parameter]
        public virtual Type CurrentDemo { get; set; }
        private void ChangeDemo(MenuItem menuItem)
        {
            CurrentDemo = null;
            switch (menuItem.Route)
            {
                case "buttons":
                    CurrentDemo = typeof(ButtonDemo);
                    break;
                case "inputs":
                    CurrentDemo = typeof(InputDemo);
                    break;
            }
            Render();
        }
    }
}
