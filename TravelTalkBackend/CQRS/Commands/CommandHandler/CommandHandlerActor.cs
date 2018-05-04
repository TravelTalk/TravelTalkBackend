namespace Commands.CommandHandler {
    using Akka.Actor;

    public class CommandHandlerActor : ReceiveActor {

        private static void ForwardCommandToWorker<TCommand, TCommandResult>(Props commandHandlerProps, TCommand command)
                where TCommand : ICommand
                where TCommandResult : ICommandResult {
            Context.ActorOf(Props.Create<CommandWorkerActor<TCommand, TCommandResult>>())
                    .Forward(new CommandWorkerActor<TCommand, TCommandResult>.CommandWorkerJob(commandHandlerProps, command));
        }
    }
}
