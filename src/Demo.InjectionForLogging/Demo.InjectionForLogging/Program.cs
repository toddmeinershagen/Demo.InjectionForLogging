using System;

using NLog;
using StructureMap;
using StructureMap.Pipeline;
using System.Linq;
using System.Collections.Generic;
using NLog.Targets;
using NLog.Config;

namespace Demo.InjectionForLogging
{
    class Program
    {
        static void Main(string[] args)
        {
            var config = new LoggingConfiguration();
            var target = new ColoredConsoleTarget();
            config.AddTarget("console", target);

            var rule = new LoggingRule("*", LogLevel.Debug, target);
            config.LoggingRules.Add(rule);

            LogManager.Configuration = config;

            var container = new Container(new ConsoleRegistry());
            var commands = new List<ICommand>();

            commands.Add(container.GetInstance<Command1>());
            commands.Add(container.GetInstance<Command1>());
            commands.Add(container.GetInstance<Command2>());

            foreach (var command in commands)
            {
                command.Execute();
            }

            Console.ReadLine();
        }
    }

    public class Command1 : ICommand
    {
        private readonly ILogger _logger;

        public Command1(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            _logger.Info("This is an informational message from Command1.");
        }
    }

    public class Command2 : ICommand
    {
        private readonly ILogger _logger;

        public Command2(ILogger logger)
        {
            _logger = logger;
        }

        public void Execute()
        {
            _logger.Error("This is an error message from Command2.");
        }
    }

    public interface ICommand
    {
        void Execute();
    }

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

    public class ConsoleRegistry : Registry
    {
        public ConsoleRegistry()
        {
            Scan(scan =>
            {
                scan.TheCallingAssembly();
                scan.WithDefaultConventions();
            });
            Policies.Add<AddLoggerPolicy>();
        }
    }
}
