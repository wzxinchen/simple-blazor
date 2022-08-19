using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleBlazor.JavaScript
{
    public static class Location
    {
        public static string Hash { get => Runtime.InvokeJS("location.hash"); set => Runtime.InvokeJS("location.hash='" + value + "'"); }
    }
}
