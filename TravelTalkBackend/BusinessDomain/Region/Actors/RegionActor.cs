namespace BusinessDomain.Region.Actors {
    using System.Threading.Tasks;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka.Actor;
    using Akka.Cluster.Sharding;
    using DomainUtilities;
    using States;

    public sealed class RegionActor : AbstractEntityActor<RegionState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        public static Task<IActorRef> StartShardRegion(ActorSystem system) {
            return ClusterSharding.Get(system).StartAsync(
                    nameof(RegionActor),
                    Props.Create<RegionActor>(),
                    ClusterShardingSettings.Create(system),
                    new MessageExtractor());
        }

        protected override void UpdateState(IDomainEvent domainEvent) {
            // TODO_IMPLEMENT_ME();
        }
    }
}
