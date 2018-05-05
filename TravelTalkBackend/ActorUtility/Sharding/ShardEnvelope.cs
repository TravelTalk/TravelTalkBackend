namespace ActorUtilityTravelTalk.ActorUtility.Sharding {
    using AtLeastOneDelivery;

    public sealed class ShardEnvelop<TMessage> : IShardEnvelop {

        private ShardEnvelop(string entityId, TMessage message) {
            EntityId = entityId;
            Message = message;
        }

        public static ShardEnvelop<TMessage> Create(string entityId, TMessage message) {
            return new ShardEnvelop<TMessage>(entityId, message);
        }

        public string EntityId { get; }

        public TMessage Message { get; }
        
        object IShardEnvelop.Message => Message;
    }
}
