using Microsoft.AspNetCore.Mvc;


namespace WebApi.Controllers {
    using System.Collections.Generic;
    using Akka.Actor;
    using Model;
    using Model.Message;

    public class MessageController : ApiBaseController {
        // GET
        
        [HttpGet]
        public IActionResult GetMessages(GetMessagesQuery query) {
            return Ok(new GetMessagesResult {
                    Messages = new List<MessageRemoteDto>(),
                    UserCount = 10
            });
        }

        public MessageController(ActorSystem actorSystem) : base(actorSystem) { }
    }
}
