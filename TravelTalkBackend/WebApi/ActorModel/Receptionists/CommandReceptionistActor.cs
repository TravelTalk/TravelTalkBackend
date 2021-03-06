﻿namespace TravelTalk.WebApi.ActorModel.Receptionists {
    using System;
    using ActorUtilityTravelTalk.ActorUtility.Extensions;
    using Akka.Actor;
    using Commands.CommandHandler;
    using Commands.GeneralCommandEvents;
    using ConstantContent;

    internal sealed class CommandReceptionistActor<TCommand, TCommandResult> : ReceiveActor
            where TCommand : ICommand<TCommandResult>
            where TCommandResult : ICommandResult {

        private readonly ActorSelection commandHandler;
        private IActorRef originalSender;

        public CommandReceptionistActor() {
            commandHandler = Context.ActorSelection(ActorSystemValues.COMMAND_HANDLER_ROUTER_PATH);

            Receive<TCommand>(command => {
                originalSender = Sender;
                commandHandler.Tell(command);

                SetReceiveTimeout(TimeSpan.FromSeconds(3));
                
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
