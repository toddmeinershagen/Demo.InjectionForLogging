using System;

using NLog;
using StructureMap;
using StructureMap.Pipeline;
using System.Linq;

namespace Demo.InjectionForLogging
{
    public class AddLoggerPolicy : ConfiguredInstancePolicy
    {
        protected override void apply(Type pluginType, IConfiguredInstance instance)
        {
            var parameter = instance.Constructor.GetParameters()
                .FirstOrDefault(p => p.ParameterType == typeof(ILogger));

            if (parameter != null)
            {
                var logger = (ILogger)LogManager.GetLogger(pluginType.ToString(), typeof(ILogger));
                instance.Dependencies.AddForConstructorParameter(parameter, logger);
            }
        }
    }
}
