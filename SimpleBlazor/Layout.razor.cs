using SimpleBlazor.Antd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor
{
    public partial class Layout
    {
        public Layout()
        {
        }
        private void ChangeDemo(MenuItem menuItem)
        {
            Data.CurrentDemo = menuItem.Route;
            Render();
        }
    }
}
