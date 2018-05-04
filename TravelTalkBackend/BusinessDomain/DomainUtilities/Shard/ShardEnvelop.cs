namespace BusinessDomain.DomainUtilities.Shard {
    using ActorUtilityTravelTalk.ActorUtility.AtLeastOneDelivery;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;

    public class ShardEnvelop<TMessage> : IShardEnvelop {

        private ShardEnvelop(string entityId, TMessage message) {
            EntityId = entityId;
            Message = message;
        }

        internal TMessage Message { get; }

        public static ShardEnvelop<TMessage> CreateEnvelop(string entityId, TMessage message) {
            return new ShardEnvelop<TMessage>(entityId, message);
        }

        public static ShardEnvelop<IMessageWithShardEntity> CreateEnvelop(IMessageWithShardEntity message) {
            return new ShardEnvelop<IMessageWithShardEntity>(message.EntityId, message);
        }

        public string EntityId { get; }

        object IShardEnvelop.Message => Message;
    }

    public class GuaranteeShardEnvelopedMessage : IShardEnvelop {

        public GuaranteeShardEnvelopedMessage(long deliveryId, IShardEnvelop defaultShardEnvelop) {
            Message = new GuaranteedMessage(deliveryId, defaultShardEnvelop.Message);
            EntityId = defaultShardEnvelop.EntityId;
        }

        public string EntityId { get; }

        public object Message { get; }
    }
}
