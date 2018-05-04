namespace Feitzinger.TyrolSky.Utility.InitializationHelper.Logging {
    using Serilog;
    using Serilog.Core;

    public class LoggerInitialization {
        public static Logger InitLogger() {
            Logger logger = new LoggerConfiguration()
                    .MinimumLevel.Information()
                    .WriteTo.ColoredConsole()
                    .WriteTo.Seq("http://localhost:5341")
                    .CreateLogger();

            Log.Logger = logger;
            return logger;
        }
    }
}
