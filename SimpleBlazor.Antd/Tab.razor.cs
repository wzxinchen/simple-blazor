using Microsoft.AspNetCore.Components;
using SimpleBlazor.Antd.EventArg;
using SimpleBlazor.JavaScript;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.Antd
{
    public partial class Tab
    {
        private List<TabHeader> _tabHeaders = new List<TabHeader>();
        private List<TabPanel> _tabPanels = new List<TabPanel>();
        [Parameter]
        public RenderFragment Header { get; set; }

        [Parameter]
        public RenderFragment Panel { get; set; }

        [Parameter]
        public Action<TabChangeEventArg<TabHeader>> OnChange { get; set; }

        internal void RegisterTabHeader(TabHeader tabHeader)
        {
            _tabHeaders.Add(tabHeader);
        }
        internal void RegisterTabPanel(TabPanel tabPanel)
        {
            _tabPanels.Add(tabPanel);
        }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "BL0005:Component parameter should not be set outside of its component.", Justification = "<挂起>")]
        internal void Change(TabHeader nextTabHeader)
        {
            var headerIndex = _tabHeaders.IndexOf(nextTabHeader);
            for (int i = 0; i < _tabPanels.Count; i++)
            {
                var tabPanel = _tabPanels[i];
                tabPanel.IsActive = i == headerIndex;
                if (!tabPanel.IsActive)
                {
                    continue;
                }
                var html = tabPanel.RenderToHtml();
                tabPanel.GetContainer().Html(html);
            }

            var headers = new List<string>();
            foreach (var tabHeader in _tabHeaders)
            {
                tabHeader.IsActive = tabHeader == nextTabHeader;
                var html = tabHeader.RenderToHtml();
                headers.Add(html);
            }

            nextTabHeader.GetContainer().Html(string.Join(string.Empty, headers));
        }
    }
}
