namespace BusinessDomain.LocationGroup.Actors {
    using System.Threading.Tasks;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka.Actor;
    using Akka.Cluster.Sharding;
    using DomainUtilities;
    using States;

    internal sealed class LocationGroupActor : AbstractEntityActor<LocationGroupState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        public static Task<IActorRef> StartShardRegion(ActorSystem system) {
            return ClusterSharding.Get(system).StartAsync(
                    nameof(LocationGroupActor),
                    Props.Create<LocationGroupActor>(),
                    ClusterShardingSettings.Create(system),
                    new MessageExtractor());

        }

        protected override void UpdateState(IDomainEvent domainEvent) {
            //            domainEvent.Match()
            //                    .With<>()
        }
    }
}
