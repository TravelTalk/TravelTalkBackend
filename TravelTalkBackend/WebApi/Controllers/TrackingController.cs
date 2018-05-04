using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers {
    using Model;
    using Model.Tracking;

    [Route("api/[controller]")]
    public class TrackingController : Controller {
        
        [HttpPost]
        public IActionResult SetLocation(SetLocationModel model) {
            return Ok();
        }
    }
}
