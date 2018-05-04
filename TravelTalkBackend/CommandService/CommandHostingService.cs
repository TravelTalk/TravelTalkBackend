namespace TravelTalk.CommandService {
    using System.IO;
    using System.Threading.Tasks;
    using Akka.Actor;
    using Akka.Configuration;
    using ConstantContent;
    using Feitzinger.TyrolSky.Utility.InitializationHelper.Logging;

    public class CommandService {

        private ActorSystem actorSystem;

        public Task TerminationHandle => actorSystem.WhenTerminated;

        public void Start() {
            actorSystem = LaunchActorSystem();

            //todo start root actors
        }

        public async Task CoordinatedShutdown() {
            await Akka.Actor.CoordinatedShutdown.Get(actorSystem).Run();
        }

        private static ActorSystem LaunchActorSystem() {
            LoggerInitialization.InitLogger();
            Config clusterConfig = ConfigurationFactory.ParseString(
                    File.ReadAllText(ResourceFileNames.COMMAND_SERVICE_CONFIG_FILE));

            return ActorSystem.Create(ActorSystemValues.ACTOR_SYSTEM_NAME, clusterConfig);
        }
    }
}
