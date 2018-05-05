namespace TravelTalk.ApplicationService.Tracking.Commands.SetLocation {
    using Akka.Actor;
    using Akka.Event;

    public sealed class SetLocationHandlerActor : ReceiveActor {

        public SetLocationHandlerActor() {
            Receive<SetLocationCommand>(c => HandleSetLocation(c));
        }

        private void HandleSetLocation(SetLocationCommand command) {
            Context.GetLogger().Info(command.ToString());
        }
    }
}
