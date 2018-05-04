namespace TravelTalk.WebApi.Controllers {
    using Akka.Actor;
    using Microsoft.AspNetCore.Mvc;
    using Model.Tracking;

    [Route("api/[controller]")]
    public class TrackingController : ApiBaseController {

        public TrackingController(ActorSystem actorSystem) : base(actorSystem) { }

        [HttpPost]
        public IActionResult SetLocation(SetLocationModel model) {
            return Ok();
        }
    }
}
