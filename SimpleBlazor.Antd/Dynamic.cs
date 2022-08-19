using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public class Dynamic : SimpleComponentBase
    {
        [Parameter]
        public Type ComponentType { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (ComponentType == null)
            {
                return;
            }
            builder.OpenComponent(0, ComponentType);
            builder.CloseComponent();
        }
    }
}
