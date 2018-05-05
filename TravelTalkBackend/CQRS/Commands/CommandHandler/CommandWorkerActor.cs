namespace Commands.CommandHandler {
    using System;
    using Akka.Actor;
    using GeneralCommandEvents;

    public sealed class CommandWorkerActor<TCommand, TCommandResult> : ReceiveActor
            where TCommand : ICommand<TCommandResult>
            where TCommandResult : ICommandResult {

        private TCommand command;
        private IActorRef commandSender;

        public CommandWorkerActor() {
            Receive<ExecuteCommand<TCommand, TCommandResult>>(x => {
                commandSender = Sender;
                command = x.Command;
                Context.ActorOf(x.ActorProps).Tell(command);
                SetReceiveTimeout(TimeSpan.FromSeconds(20));

                Become(WaitForAnswer);
            });
        }

        private void WaitForAnswer() {
            Receive<ReceiveTimeout>(x => {
                commandSender.Tell(CommandHandled<TCommand, TCommandResult>.CreateUnsuccessfulResult(command));
                Self.Tell(PoisonPill.Instance);
            });

            Receive<CommandNotSuccessfulResult>(x => {
                commandSender.Tell(CommandHandled<TCommand, TCommandResult>.CreateUnsuccessfulResult(command, x.CommandStatus));
                Self.Tell(PoisonPill.Instance);
            });

            Receive<CommandHandled<TCommand, TCommandResult>>(x => {
                commandSender.Tell(x);
                Self.Tell(PoisonPill.Instance);
            });

            Receive<TCommandResult>(x => {
                commandSender.Tell(CommandHandled<TCommand, TCommandResult>.CreateSuccessfulResult(command, x));
                Self.Tell(PoisonPill.Instance);
            });
        }

    }

}
