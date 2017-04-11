using System;
using Jint;
using System.IO;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Collections.Generic;
using Microsoft.CSharp;
using System.CodeDom.Compiler;
using Interfaces;

namespace spacy
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadAndRunJS();

            LoadAndRunCS();

            Console.ReadKey();
        }

        private static void LoadAndRunJS()
        {
            var seeker = new HeatSeeker();

            var engine = new Engine();
            engine.SetValue("log", new Action<object>(Console.WriteLine));
            engine.SetValue("heatSeeker", seeker);
            var scriptjs = File.ReadAllText("script.js");
            engine.Execute(scriptjs);
        }

        private static void LoadAndRunCS()
        {
            string source = File.ReadAllText("SomeComponent.csx");

            Dictionary<string, string> providerOptions = new Dictionary<string, string>
                {
                    {"CompilerVersion", "v3.5"}
                };
            CSharpCodeProvider provider = new CSharpCodeProvider(providerOptions);

            CompilerParameters compilerParams = new CompilerParameters
            {
                GenerateInMemory = true,
                GenerateExecutable = false
            };
            compilerParams.ReferencedAssemblies.Add(typeof(IComponent).Assembly.Location);

            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, source);

            if (results.Errors.Count != 0)
                if (results.Errors.Count > 0)
                {
                    foreach (CompilerError error in results.Errors)
                        Console.WriteLine(error);
                    Console.ReadKey();
                    return;
                }

            var o = (IComponent)results.CompiledAssembly.CreateInstance("SomeComponent");
            int ret = o.DoWork();

            Console.WriteLine(ret);
        }
    }
}
