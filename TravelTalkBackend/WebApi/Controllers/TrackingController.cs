using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers {
    using Model;
    using Model.Tracking;

    public class TrackingController : Controller {
        
        [HttpPost]
        public IActionResult SetLocation(SetLocationModel model) {
            return Ok();
        }
    }
}
