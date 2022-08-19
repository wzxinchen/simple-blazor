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
        public virtual string Type { get; set; }

        [Parameter]
        public virtual string Href { get; set; }

        [Parameter]
        public virtual string Text { get; set; }
    }
}
