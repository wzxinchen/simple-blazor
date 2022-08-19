using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.JavaScript
{
    public class Element
    {
        private string nodeId;
        private string type;

        public static event Action<string> OnClick;

        public Element(string[] elementInfos)
        {
            this.nodeId = elementInfos[0];
            type = elementInfos[1];
        }

        public static void DispatchClickEvent(string handler)
        {
            OnClick?.Invoke(handler);
        }
    }
}
