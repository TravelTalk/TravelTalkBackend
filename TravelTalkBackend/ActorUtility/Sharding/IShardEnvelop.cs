namespace ActorUtilityTravelTalk.ActorUtility.Sharding {
    
    public interface IShardEnvelop {
        
        string EntityId { get; }

        object Message { get; }
    }

    public interface IMessageWithShardEntity {
        string EntityId { get; }
    }
}
