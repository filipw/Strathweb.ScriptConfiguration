using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;

namespace Strathweb.ScriptConfiguration
{
    public class ScriptConfigurationProvider : FileConfigurationProvider
    {
        private IEnumerable<Assembly> _assemblies = new[] { typeof(object).Assembly, typeof(Enumerable).Assembly };
        private IEnumerable<string> _namespaces = new[] { "System", "System.IO", "System.Linq", "System.Collections.Generic" };

        public ScriptConfigurationProvider(ScriptConfigurationSource source) : base(source) { }

        public override void Load(Stream stream)
        {
            try
            {
                using (var reader = new StreamReader(stream, detectEncodingFromByteOrderMarks: true))
                {
                    var code = reader.ReadToEnd();
                    var opts = ScriptOptions.Default.AddImports(_namespaces).AddReferences(_assemblies).AddReferences(typeof(ScriptConfiguration).Assembly);

                    var script = CSharpScript.Create(code, opts, typeof(ScriptConfiguration));
                    var config = new ScriptConfiguration();
                    var result = script.RunAsync(config).GetAwaiter().GetResult();
                    Data = config;
                }
            }
            catch (Exception e)
            {
                //todo
            }
        }
    }
}
