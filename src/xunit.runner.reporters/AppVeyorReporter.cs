using Xunit.Abstractions;

namespace Xunit.Runner.Reporters
{
    public class AppVeyorReporter : IRunnerReporter
    {
        public string Description
            => "forces AppVeyor CI mode (normally auto-detected)";

        public bool IsEnvironmentallyEnabled
            => EnvironmentHelper.GetEnvironmentVariable("APPVEYOR_API_URL") != null;

        public string RunnerSwitch
            => "appveyor";

        public IMessageSink CreateMessageHandler(IRunnerLogger logger)
        {
            var baseUri = EnvironmentHelper.GetEnvironmentVariable("APPVEYOR_API_URL");
            return baseUri == null
                ? new DefaultRunnerReporterWithTypesMessageHandler(logger)
                : new AppVeyorReporterMessageHandler(logger, baseUri);
        }
    }
}
