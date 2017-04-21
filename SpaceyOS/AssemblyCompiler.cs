using Interfaces;
using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpaceyOS
{
    class AssemblyCompiler
    {
        List<string> filenames = new List<string>();
        Assembly assembly;

        Assembly Compile()
        {
            if (filenames.Count == 0)
                return null;

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
            compilerParams.ReferencedAssemblies.Add(typeof(ISpaceShip).Assembly.Location);

            var sources = filenames.Select(fn => File.ReadAllText(fn));

            CompilerResults results = provider.CompileAssemblyFromSource(compilerParams, sources.ToArray());

            if (results.Errors.Count != 0)
                if (results.Errors.Count > 0)
                    throw new Exception(results.Errors[0].ErrorText);

            return results.CompiledAssembly;
        }

        public AssemblyCompiler(string[] filenames)
        {
            this.filenames = filenames.ToList();
            assembly = Compile();
        }

        public void AddFiles(string[] filenames)
        {
            var doesntHaveYet = filenames.Where(f => !this.filenames.Contains(f));
            this.filenames.AddRange(doesntHaveYet);
            assembly = Compile();
        }

        public void RemoveFiles(string[] filenames)
        {
            this.filenames = this.filenames.Where(f => !filenames.Contains(f)).ToList();
            assembly = Compile();
        }

        public object CreateInstance(string className)
        {
            if (assembly == null)
                return null;
            var o = assembly.CreateInstance(className);
            return o;
        }
    }
}
