namespace TravelTalk.WebApi.Controllers {
    using System.Collections.Generic;
    using Akka.Actor;
    using Microsoft.AspNetCore.Mvc;
    using Model.Message;

    public class MessageController : AbstractApiController {

        public MessageController(ActorSystem actorSystem) : base(actorSystem) { }

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
