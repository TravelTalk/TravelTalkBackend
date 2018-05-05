﻿namespace TravelTalk.ApplicationService.Comon {
    using Akka.Actor;
    using Commands.CommandHandler;
    using Tracking.Commands;

    public sealed class CommandHandlerActor : ReceiveActor {

        public CommandHandlerActor() {
            Receive<SetLocationCommand>(c => ForwardCommandToWorker<SetLocationCommand, VoidResult>(Props.Create<SetLocationHandlerActor>(), c));
        }

        private static void ForwardCommandToWorker<TCommand, TCommandResult>(Props commandHandlerProps, TCommand command)
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult {
            Context.ActorOf(Props.Create<CommandWorkerActor<TCommand, TCommandResult>>())
                    .Forward(new ExecuteCommand<TCommand, TCommandResult>(commandHandlerProps, command));
        }
    }
}
