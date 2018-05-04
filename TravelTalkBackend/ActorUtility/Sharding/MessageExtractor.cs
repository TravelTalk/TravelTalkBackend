namespace Feitzinger.TyrolSky.Utility.AkkaUtilities.Sharding {
    using System;
    using Akka.Cluster.Sharding;
    using Akka.Util;

    public class MessageExtractor : HashCodeMessageExtractor {

        //never change this value! It must be unique during the whole live of the cluster
        private static readonly int MAX_NUMBER_OF_SHARDS = 100;

        public MessageExtractor() : base(MAX_NUMBER_OF_SHARDS) { }

        public override string EntityId(object message) {
            switch (message) {
                case IShardEnvelop e: return e.EntityId;
                case ShardRegion.StartEntity start: return start.EntityId;
            }

            throw new MessageNotSupportedException();
        }

        public override string ShardId(object message) {
            string id = EntityId(message);
            int hash = MurmurHash.StringHash(id);
            return (Math.Abs(hash) % MAX_NUMBER_OF_SHARDS).ToString();
        }

        public override object EntityMessage(object message) {
            return (message as IShardEnvelop)?.Message ?? message;
        }
    }

    public class MessageNotSupportedException : Exception { }

}
