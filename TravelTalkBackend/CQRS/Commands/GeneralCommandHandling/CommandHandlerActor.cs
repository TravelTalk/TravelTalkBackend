namespace Commands.GeneralCommandHandling {
    using Akka.Actor;

    public class CommandHandlerActor : ReceiveActor {

        private static void ForwardCommandToWorker<TCommand, TCommandResult>(Props commandHandlerProps, TCommand query)
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult {
            Context.ActorOf(Props.Create<CommandWorkerActor<TCommand, TCommandResult>>())
                    .Forward(new CommandWorkerActor<TCommand, TCommandResult>.CommandWorkerJob(commandHandlerProps, query));

        }
    }
}
