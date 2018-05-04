﻿namespace TravelTalk.WebApi.Controllers {
    using System.Threading.Tasks;
    using ActorModel.Receptionists;
    using Akka.Actor;
    using Commands.GeneralCommandEvents;
    using Commands.GeneralCommandHandling;
    using Microsoft.AspNetCore.Mvc;

    public abstract class ApiBaseController : Controller {
        public ApiBaseController(ActorSystem actorSystem) {
            ActorSystem = actorSystem;
        }

        protected ActorSystem ActorSystem { get; }

        protected Task<CommandHandled<TCommand, TCommandResult>> HandleCommand<TCommand, TCommandResult>(TCommand command)
                where TCommand : ICommand<TCommandResult>
                where TCommandResult : ICommandResult {
            IActorRef receptionist = ActorSystem.ActorOf(Props.Create<CommandReceptionistActor<TCommand, TCommandResult>>());
            return receptionist.Ask<CommandHandled<TCommand, TCommandResult>>(command);
        }
    }
}
