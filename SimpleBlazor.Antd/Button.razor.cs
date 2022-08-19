using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class Button
    {
        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public string Href { get; set; }
    }
}
