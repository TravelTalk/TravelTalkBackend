namespace CommandService {
    using System.IO;
    using System.Threading.Tasks;
    using Akka.Actor;
    using Akka.Configuration;
    using ConstantContent;
    using Feitzinger.TyrolSky.Utility.InitializationHelper.Logging;

    public class DomainHostingService {

        private ActorSystem tyrolSkyActorSystem;

        public Task TerminationHandle => tyrolSkyActorSystem.WhenTerminated;

        public void Start() {
            tyrolSkyActorSystem = DomainHostingServiceHostFactory.LaunchDomainService();

            //todo start root actors
        }

        public async Task StopAsync() {
            await CoordinatedShutdown.Get(tyrolSkyActorSystem).Run();
        }

        public static class DomainHostingServiceHostFactory {
            public static ActorSystem LaunchDomainService() {
                LoggerInitialization.InitLogger();

                string configString = File.ReadAllText(ResourceFileNames.COMMAND_SERVICE_CONFIG_FILE);
                Config clusterConfig = ConfigurationFactory.ParseString(configString);

                return ActorSystem.Create(ActorSystemValues.ACTOR_SYSTEM_NAME, clusterConfig);
            }
        }
    }
}
