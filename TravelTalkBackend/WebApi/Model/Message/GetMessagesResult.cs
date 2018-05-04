namespace WebApi.Model.Message {
    using System.Collections.Generic;

    public sealed class GetMessagesResult {

        public IEnumerable<MessageRemoteDto> Messages { get; set; }

        public int UserCount { get; set; }
    }

    public sealed class MessageRemoteDto : IRemoteDto {

        public string Id { get; set; }
        
        public string SenderId { get; set; }

        public string MessageText { get; set; }
    }

    public interface IRemoteDto { }
}
