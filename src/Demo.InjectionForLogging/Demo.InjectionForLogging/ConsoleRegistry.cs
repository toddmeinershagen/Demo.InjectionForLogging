using StructureMap;

namespace Demo.InjectionForLogging
{
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
