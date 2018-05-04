namespace ActorUtilityTravelTalk.ActorUtility.Sharding {
    using System;
    using Akka.Cluster.Sharding;

    public class MessageExtractor : HashCodeMessageExtractor {

        //never change this value! It must be unique during the whole live of the cluster
        private const int MAX_NUMBER_OF_SHARDS = 100;

        public MessageExtractor() : base(MAX_NUMBER_OF_SHARDS) { }

        public override string EntityId(object message) {
            switch (message) {
                case IShardEnvelop e: return e.EntityId;
                case ShardRegion.StartEntity start: return start.EntityId;
            }

            throw new MessageNotSupportedException();
        }

        public override object EntityMessage(object message) {
            return (message as IShardEnvelop)?.Message ?? message;
        }
    }

    public class MessageNotSupportedException : Exception { }
}
