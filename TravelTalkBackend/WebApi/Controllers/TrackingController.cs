namespace TravelTalk.WebApi.Controllers {
    using System.Threading.Tasks;
    using Akka.Actor;
    using ApplicationService.Tracking.Commands.SetLocation;
    using Commands.CommandHandler;
    using Commands.GeneralCommandEvents;
    using Microsoft.AspNetCore.Mvc;
    using Model.Tracking;

    public class TrackingController : AbstractApiController {

        public TrackingController(ActorSystem actorSystem) : base(actorSystem) { }

        public async Task<IActionResult> SetLocation([FromBody] SetLocationModel model) {
            CommandHandled<SetLocationCommand, VoidResult> result = await HandleCommand<SetLocationCommand, VoidResult>(
                    new SetLocationCommand(model.UserId, model.Longitude, model.Latitude));

            if (!result.ResultStatus.Successfully) {
                return StatusCode(500, "Location cannot be set.");
            }

            return Ok();
        }
    }
}
