namespace TravelTalk.WebApi.ActorModel.Receptionists {
    using System;
    using Akka.Actor;
    using Commands.GeneralCommandEvents;
    using Commands.GeneralCommandHandling;

    internal class CommandReceptionistActor<TCommand, TCommandResult> : ReceiveActor
            where TCommand : ICommand<TCommandResult>
            where TCommandResult : ICommandResult {

        private readonly ActorSelection commandHandler;
        private IActorRef originalSender;

        public CommandReceptionistActor() {
            commandHandler = Context.ActorSelection("/user/commandHandlerRouter");

            Receive<TCommand>(command => {
                originalSender = Sender;
                commandHandler.Tell(command);

                SetReceiveTimeout(TimeSpan.FromSeconds(30));

                Become(() => {
                    Receive<CommandHandled<TCommand, TCommandResult>>(x => {
                        originalSender.Tell(x);
                        Self.Tell(PoisonPill.Instance);
                    });
                    Receive<ReceiveTimeout>(x => {
                        originalSender.Tell(CommandHandled<TCommand, TCommandResult>.CreateUnsuccessfulResult(command, CommandResultStatus.GENERAL_ERROR));
                    });
                });
            });
        }
    }
}
