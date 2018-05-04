namespace TravelTalk.CommandService {
    using System.IO;
    using System.Threading.Tasks;
    using Akka.Actor;
    using Akka.Configuration;
    using Akka.Routing;
    using Commands.CommandHandler;
    using ConstantContent;

    public class CommandService {

        private ActorSystem actorSystem;

        public Task TerminationHandle => actorSystem.WhenTerminated;

        public void Start() {
            actorSystem = LaunchActorSystem();

            actorSystem.ActorOf(Props.Create<CommandHandlerActor>().WithRouter(FromConfig.Instance), 
                    ActorConstants.COMMAND_HANDLER_POOL_ROUTER_NAME);

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
