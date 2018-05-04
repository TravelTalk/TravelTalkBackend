using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers {
    using System.Collections.Generic;
    using Model;
    using Model.Message;

    public class MessageController : Controller {
        // GET
        
        [HttpGet]
        public IActionResult GetMessages(GetMessagesQuery query) {
            return Ok(new GetMessagesResult {
                    Messages = new List<MessageRemoteDto>(),
                    UserCount = 10
            });
        }
    }
}
