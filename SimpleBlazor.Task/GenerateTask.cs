using Microsoft.AspNetCore.Razor.Language;
using System;
using System.Collections.Generic;
using System.IO;

namespace SimpleBlazor.Task
{
    public class GenerateTask : Microsoft.Build.Utilities.Task
    {
        public override bool Execute()
        {
            Log.LogMessage("SimpleBlazor 将 cshtml 编译至 csharp 代码");
            Log.LogMessage(Directory.GetCurrentDirectory());
            Tranlsate("cshtml");
            Tranlsate("razor");
            return true;
        }

        private void Tranlsate(string fileType)
        {
            var razorPages = System.IO.Directory.GetFiles(Directory.GetCurrentDirectory(), "*." + fileType, SearchOption.AllDirectories);
            var razorProject = RazorProjectEngine.Create(RazorConfiguration.Default, RazorProjectFileSystem.Create("."));
            var importRazorCodes = new List<RazorSourceDocument>() {
            RazorSourceDocument.Create("@inherits RazorEngineCore.RazorEngineTemplateBase"+Environment.NewLine+"<button type=\"button\" class=\"ant-btn ant-btn-primary\"><span>Primary Button</span></button>", "SimpleBlazor.Antd.TestButton")
        };
            foreach (var razorPage in razorPages)
            {
                var razorCode = File.ReadAllText(razorPage);
                if (!razorCode.StartsWith("@inherits"))
                {
                    razorCode = "@inherits RazorEngineCore.RazorEngineTemplateBase" + Environment.NewLine + razorCode;
                }
                var source = RazorSourceDocument.Create(razorCode, razorPage);
                var csharpCode = razorProject.Process(source, null, importRazorCodes, new List<TagHelperDescriptor>()).GetCSharpDocument().GeneratedCode;
                var fileName = Path.GetFileNameWithoutExtension(razorPage);
                csharpCode = csharpCode.Replace("Template :", fileName + " :");
                var destPath = Path.Combine(Path.GetDirectoryName(razorPage), fileName + "." + fileType + ".cs");
                File.WriteAllText(destPath, csharpCode);
                Log.LogMessage(razorPage + " -> " + destPath);
            }
        }
    }
}
