using System;

using NLog;
using StructureMap;
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
}
