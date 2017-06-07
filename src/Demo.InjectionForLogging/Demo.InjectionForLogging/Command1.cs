
using NLog;

namespace Demo.InjectionForLogging
{
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
}
