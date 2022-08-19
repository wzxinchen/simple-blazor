using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class TabHeader : IOwnerBy
    {
        [Parameter]
        public string Title { get; set; }

        [Parameter]
        public object Owner { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool IsActive { get; set; }

        [Parameter]
        public Action OnClick { get; set; }

        protected override void OnParametersSet()
        {
            if (Owner is Tab tab)
            {
                tab.RegisterTabHeader(this);
            }
        }

        private void OnClickInternal()
        {
            OnClick?.Invoke();
            if(Owner is Tab tab)
            {
                tab.Change(this);
            }
        }
    }
}
