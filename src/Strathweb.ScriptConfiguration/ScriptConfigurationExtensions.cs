using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Strathweb.ScriptConfiguration
{
    public static class ScriptConfigurationExtensions
    {
        public static IConfigurationBuilder AddScriptFile(this IConfigurationBuilder builder, string path, bool optional = false, bool reloadOnChange = false)
        {
            if (builder == null)
            {
                throw new ArgumentNullException(nameof(builder));
            }

            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("Missing file path!");
            }

            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, path);
            }

            var source = new ScriptConfigurationSource
            {
                FileProvider = new PhysicalFileProvider(Path.GetDirectoryName(path)),
                Path = Path.GetFileName(path),
                Optional = optional,
                ReloadOnChange = reloadOnChange
            };
            builder.Add(source);
            return builder;
        }
    }
}
