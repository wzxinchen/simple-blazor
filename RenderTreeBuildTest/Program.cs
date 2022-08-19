// See https://aka.ms/new-console-template for more information
using Castle.DynamicProxy;
using SimpleBlazor;
using SimpleBlazor.Antd;
using SimpleBlazor.Pages;

ProxyGenerator _proxyGenerator = new ProxyGenerator();
PropertyInterceptor _propertyInterceptor = new PropertyInterceptor();
var a = _proxyGenerator.CreateClassProxy<TabDemo>(_propertyInterceptor);
var c = a.GetType().BaseType.GetMethods(
    System.Reflection.BindingFlags.Instance |
    System.Reflection.BindingFlags.NonPublic |
    System.Reflection.BindingFlags.InvokeMethod ).FirstOrDefault(x => x.Name == "Test");
Console.WriteLine("Hello, World!");
var router1 = new TabDemo();
var html = router1.RenderToHtml();
Console.WriteLine(html);