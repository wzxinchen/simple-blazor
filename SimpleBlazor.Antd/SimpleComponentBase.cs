using Castle.DynamicProxy;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;
using SimpleBlazor.JavaScript;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#pragma warning disable BL0006 // Do not use RenderTree types
namespace SimpleBlazor.Antd
{
    public class SimpleComponentBase<TModel> : SimpleComponentBase
        where TModel : class, new()
    {
        private static ProxyGenerator _proxyGenerator = new ProxyGenerator();
        internal PropertyInterceptor _propertyInterceptor = new PropertyInterceptor();
        public TModel Data { get; }

        public SimpleComponentBase()
        {
            Data = _proxyGenerator.CreateClassProxy<TModel>(_propertyInterceptor);
        }

        protected void Render()
        {
            if (_propertyInterceptor.StateHasChanged)
            {
                Console.WriteLine("StateHasChanged");
                var html = RenderToHtml();
                GetContainer().Html(html);
            }
        }
    }
    public class SimpleComponentBase : ComponentBase, IDisposable
    {
        internal virtual string ParentSelector { get; set; }
        internal static IDictionary<int, object> ComponentsMap { get; } = new Dictionary<int, object>();
        public SimpleComponentBase()
        {
            ComponentsMap.Add(GetHashCode(), this);
        }

        public class KlassBuilder
        {
            private List<string> klasses = new List<string>();

            public KlassBuilder Append(string klassName)
            {
                klasses.Add(klassName);
                return this;
            }
            public KlassBuilder A(string klassName)
            {
                return Append(klassName);
            }
            public KlassBuilder AppendIf(string klassName, bool condition)
            {
                if (!condition)
                {
                    return this;
                }
                klasses.Add(klassName);
                return this;
            }
            public KlassBuilder AF(string klassName, bool condition)
            {
                return AppendIf(klassName, condition);
            }

            public override string ToString()
            {
                return string.Join(" ", klasses);
            }
        }
        protected KlassBuilder Klass => new KlassBuilder();

        internal DomQueryable GetContainer()
        {
            if (string.IsNullOrWhiteSpace(ParentSelector))
            {
                throw new InvalidOperationException("ParentSelector为空");
            }
            if (ParentSelector == "body")
            {
                return Document.Body;
            }
            return Document.Body.QuerySelector(ParentSelector);
        }
        /// <summary>
        /// 将 RenderTreeBuilder 渲染成 HTML，然后直接显示
        /// </summary>
        /// <returns></returns>
        public string RenderToHtml()
        {
            var builder = new RenderTreeBuilder();
            BuildRenderTree(builder);
            var htmlBuilder = new StringBuilder();
            var openStack = new Stack<FrameInfo>();
            var frames = builder.GetFrames().Array;
            foreach (var frame in frames)
            {
                var currentIndex = Array.IndexOf(frames, frame);
                var nextIndex = currentIndex + 1;
                RenderTreeFrame nextFrame = default;
                if (nextIndex < frames.Length)
                {
                    nextFrame = frames[nextIndex];
                }
                FrameInfo previousFrame = default;
                if (openStack.Any())
                {
                    previousFrame = openStack.Peek();
                    TryCloseFrame(htmlBuilder, openStack, currentIndex, previousFrame);
                }
                var id = Guid.NewGuid().ToString();
                switch (frame.FrameType)
                {
                    case RenderTreeFrameType.Element:
                        htmlBuilder.Append(Environment.NewLine).Append('<').Append(frame.ElementName);
                        openStack.Push(new FrameInfo(frame, currentIndex + frame.ElementSubtreeLength, new Dictionary<string, object>(), id));
                        if (nextFrame.FrameType != RenderTreeFrameType.Attribute)
                        {
                            htmlBuilder.Append('>');
                        }
                        break;
                    case RenderTreeFrameType.Attribute:
                        if (previousFrame.OpenFrame.FrameType == RenderTreeFrameType.Element)
                        {
                            htmlBuilder.Append(' ').Append(frame.AttributeName).Append('=');
                            if (frame.AttributeValue is Delegate action)
                            {
                                htmlBuilder.Append('"').Append(frame.AttributeName + "Handler('" + GetHashCode() + ":" + action.Method.Name + "')").Append('"');
                            }
                            else if (frame.AttributeValue is EventCallback eventCallback)
                            {
                            }
                            else
                            {
                                htmlBuilder.Append('"').Append(frame.AttributeValue).Append('"');
                            }
                            if (nextFrame.FrameType == RenderTreeFrameType.Region)
                            {
                                htmlBuilder.Append("_id=").Append('"').Append(previousFrame.Id).Append('"');
                            }
                            if (nextFrame.FrameType != RenderTreeFrameType.Attribute)
                            {
                                htmlBuilder.Append('>');
                            }
                        }
                        else
                        {
                            previousFrame.Attributes.Add(frame.AttributeName, frame.AttributeValue);
                        }
                        break;
                    case RenderTreeFrameType.Markup:
                        htmlBuilder.Append(frame.MarkupContent);
                        break;
                    case RenderTreeFrameType.Text:
                        htmlBuilder.Append(frame.TextContent);
                        break;
                    case RenderTreeFrameType.Component:
                        if (frame.Component is SimpleComponentBase simpleComponent)
                        {
                            var componentHtml = simpleComponent.RenderToHtml();
                            htmlBuilder.Append(componentHtml);
                        }
                        else if (frame.ComponentType.BaseType == typeof(SimpleComponentBase))
                        {
                            openStack.Push(new FrameInfo(frame, currentIndex + frame.ComponentSubtreeLength, new Dictionary<string, object>(), previousFrame.Id));
                        }
                        break;
                }
            }
            return htmlBuilder.ToString();
        }

        private void TryCloseFrame(StringBuilder htmlBuilder, Stack<FrameInfo> openStack, int currentIndex, FrameInfo previousFrame)
        {
            var whilePreviousFrame = previousFrame;
            while (currentIndex == whilePreviousFrame.ClosingIndex || (currentIndex == 0 && whilePreviousFrame.ClosingIndex == 1))
            {
                openStack.Pop();
                switch (whilePreviousFrame.OpenFrame.FrameType)
                {
                    case RenderTreeFrameType.Element:
                        htmlBuilder.Append("</").Append(whilePreviousFrame.OpenFrame.ElementName).Append('>');
                        break;
                    case RenderTreeFrameType.Component:
                        var simpleComponentByType = (SimpleComponentBase)Activator.CreateInstance(whilePreviousFrame.OpenFrame.ComponentType);
                        simpleComponentByType.OnInitialized();
                        simpleComponentByType.ParentSelector = $"[_id=\"{whilePreviousFrame.Id}\"]";
                        var attrs = whilePreviousFrame.Attributes.ToDictionary(x => x.Key, x => x.Value);
                        if (simpleComponentByType is IOwnerBy)
                        {
                            attrs.Add(nameof(IOwnerBy.Owner), this);
                        }
                        var parameterView = ParameterView.FromDictionary(attrs);
                        parameterView.SetParameterProperties(simpleComponentByType);
                        simpleComponentByType.OnParametersSet();
                        var componentHtml = simpleComponentByType.RenderToHtml();
                        simpleComponentByType.OnAfterRender(false);
                        htmlBuilder.Append(componentHtml);
                        break;
                }
                if (!openStack.Any())
                {
                    break;
                }
                whilePreviousFrame = openStack.Peek();
            }
        }

        public void Dispose()
        {
            ComponentsMap.Remove(GetHashCode());
        }

        public static SimpleComponentBase GetComponent(int hashCode)
        {
            ComponentsMap.TryGetValue(hashCode, out var obj);
            return (SimpleComponentBase)obj;
        }
    }

    internal record struct FrameInfo(RenderTreeFrame OpenFrame, int ClosingIndex, IDictionary<string, object> Attributes, string Id)
    {
    }
}

#pragma warning restore BL0006 // Do not use RenderTree types