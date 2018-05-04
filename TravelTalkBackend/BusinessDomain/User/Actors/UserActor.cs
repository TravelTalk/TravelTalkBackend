namespace BusinessDomain.User.Actors {
    using System.Threading.Tasks;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka.Actor;
    using Akka.Cluster.Sharding;

    public sealed class UserActor : ReceiveActor {

        public static Task<IActorRef> StartShardRegion(ActorSystem system) {
            return ClusterSharding.Get(system).StartAsync(
                    nameof(UserActor),
                    Props.Create<UserActor>(),
                    ClusterShardingSettings.Create(system),
                    new MessageExtractor());
        }
    }
}
