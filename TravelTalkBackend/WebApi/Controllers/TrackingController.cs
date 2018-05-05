namespace TravelTalk.WebApi.Controllers {
    using System.Threading.Tasks;
    using Akka.Actor;
    using Commands.CommandHandler;
    using Commands.GeneralCommandEvents;
    using CommandService.SetLocation;
    using Microsoft.AspNetCore.Mvc;
    using Model.Tracking;

    [Route("api/[controller]")]
    public class TrackingController : AbstractApiController {

        public TrackingController(ActorSystem actorSystem) : base(actorSystem) { }

        [HttpPost]
        public async Task<IActionResult> SetLocation(SetLocationModel model) {

            CommandHandled<SetLocationCommand, EmptyCommandResult> result = await HandleCommand<SetLocationCommand, EmptyCommandResult>(
                    new SetLocationCommand(model.UserId, model.Longitude, model.Latitude));

            if (!result.ResultStatus.Successfully) {
                return StatusCode(500, "Flight reservation currently not possible!");
            }

            return Ok();
        }
    }
}
