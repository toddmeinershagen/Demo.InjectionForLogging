
using NLog;

namespace Demo.InjectionForLogging
{
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
}
