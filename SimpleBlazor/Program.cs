using SimpleBlazor.Antd;
using SimpleBlazor.JavaScript;
using SimpleBlazor.Pages;
using System;
using System.Collections.Generic;

namespace SimpleBlazor
{
    public class Program
    {
        private static IDictionary<string, Type> routers = new Dictionary<string, Type>();
        static void Main(string[] args)
        {
            Window.AddEventListener(EventType.HashChange, () =>
            {
                var html = Render(Location.Hash);
                Document.Body.QuerySelector("#page_content").Html(html);
            });
            Element.OnClick += Element_OnClick;
            routers.Add("#buttons", typeof(ButtonDemo));
            routers.Add("#tabs", typeof(TabDemo));
            //routers.Add("#playground", typeof(router2));
            var template = new Layout();
            var html = template.RenderToHtml();
            Document.Body.Html(html);
            //Console.WriteLine("Hello, World!");
        }

        private static void Element_OnClick(string obj)
        {
            var hashCode = Convert.ToInt32(obj.Split(':')[0]);
            var component = SimpleComponentBase.GetComponent(hashCode);
            if (component == null)
            {
                return;
            }
            var method = component.GetType().GetMethod(obj.Split(':')[1], System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod);
            method.Invoke(component, null);
        }

        public static string Render(string router)
        {
            var type = routers[router];
            var template = (SimpleComponentBase)Activator.CreateInstance(type);
            var html = template.RenderToHtml();
            return html;
        }
    }
}