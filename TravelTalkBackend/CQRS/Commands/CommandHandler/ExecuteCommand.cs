namespace Commands.CommandHandler {
    using Akka.Actor;

    public sealed class ExecuteCommand<TCommand, TCommandResult> 
            where TCommand : ICommand<TCommandResult> 
            where TCommandResult : ICommandResult {

        public ExecuteCommand(Props actorProps, TCommand command) {
            ActorProps = actorProps;
            Command = command;
        }

        public Props ActorProps { get; }

        public TCommand Command { get; }
    }
}