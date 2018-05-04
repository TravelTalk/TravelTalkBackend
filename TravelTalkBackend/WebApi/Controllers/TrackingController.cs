namespace TravelTalk.WebApi.Controllers {
    using System.Threading.Tasks;
    using Akka.Actor;
    using Microsoft.AspNetCore.Mvc;
    using Model.Tracking;

    [Route("api/[controller]")]
    public class TrackingController : AbstractApiController {

        public TrackingController(ActorSystem actorSystem) : base(actorSystem) { }

        [HttpPost]
        public async Task<IActionResult> SetLocation(SetLocationModel model) {

            var result = await HandleCommand<object, object>(new object());

            if (!result.ResultStatus.Successfully) {
                return StatusCode(500, "Flight reservation currently not possible!");
            }

            return Ok();
        }
    }
}
