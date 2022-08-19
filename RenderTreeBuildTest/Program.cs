// See https://aka.ms/new-console-template for more information
using SimpleBlazor;
using SimpleBlazor.Pages;

Console.WriteLine("Hello, World!");
var router1 = new TabDemo();
var html = router1.RenderToHtml();
Console.WriteLine(html);