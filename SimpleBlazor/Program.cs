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
            template.Render(true);
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

            var methodName = obj.Split(':')[1];
            var componentType = component.GetType();
            if (componentType.Name.EndsWith("Proxy"))
            {
                componentType = componentType.BaseType;
            }
            var method = componentType.GetMethod(methodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.InvokeMethod);
            if (method == null)
            {
                throw new Exception(componentType.Name + "." + methodName + "，未找到");
            }
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