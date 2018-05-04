using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers {
    using Akka.Actor;
    using Model;
    using Model.Tracking;

    [Route("api/[controller]")]
    public class TrackingController : ApiBaseController {
        
        [HttpPost]
        public IActionResult SetLocation(SetLocationModel model) {
            return Ok();
        }

        public TrackingController(ActorSystem actorSystem) : base(actorSystem) { }
    }
}
