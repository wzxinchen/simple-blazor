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
        public virtual string Title { get; set; }

        [Parameter]
        public virtual object Owner { get; set; }

        [Parameter]
        public virtual bool IsActive { get; set; }

        [Parameter]
        public virtual Action OnClick { get; set; }

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
