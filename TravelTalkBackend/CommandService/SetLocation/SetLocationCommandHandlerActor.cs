namespace TravelTalk.CommandService.SetLocation {
    using Akka.Actor;
    using Akka.Event;

    public class SetLocationCommandHandlerActor : ReceiveActor {

        public SetLocationCommandHandlerActor() {
            Receive<SetLocationCommand>(c => HandleSetLocation(c));
        }

        private void HandleSetLocation(SetLocationCommand command) {
            Context.GetLogger().Info(command);
        }
    }
}
