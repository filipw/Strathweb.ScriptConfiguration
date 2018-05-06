using Microsoft.Extensions.Configuration;
using System.Text;

namespace Strathweb.ScriptConfiguration
{
    public class ScriptConfigurationSource : FileConfigurationSource
    {
        public override IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            FileProvider = FileProvider ?? builder.GetFileProvider();
            return new ScriptConfigurationProvider(this);
        }
    }
}
