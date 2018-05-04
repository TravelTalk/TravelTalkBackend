namespace Feitzinger.TyrolSky.Utility.AkkaUtilities.Sharding {
    public interface
            IShardEnvelop {
        string EntityId { get; }

        object Message { get; }
    }

    public interface IMessageWithShardEntity {
        string EntityId { get; }
    }
}
