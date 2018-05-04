namespace Commands.CommandHandler {
    using System;
    using Akka.Actor;
    using GeneralCommandEvents;

    internal sealed class CommandWorkerActor<TCommand, TCommandResult> : ReceiveActor
            where TCommand : ICommand
            where TCommandResult : ICommandResult {
        
        private TCommand command;
        private IActorRef commandSender;

        public CommandWorkerActor() {
            Receive<CommandWorkerJob>(x => {
                commandSender = Sender;
                command = x.Command;
                Context.ActorOf(x.Props).Tell(command);
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

        public class CommandWorkerJob {

            public CommandWorkerJob(Props props, TCommand command) {
                Props = props;
                Command = command;
            }

            public Props Props { get; }

            public TCommand Command { get; }
        }
    }
}
