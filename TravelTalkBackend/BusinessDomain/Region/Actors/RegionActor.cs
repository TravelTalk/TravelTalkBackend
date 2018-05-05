namespace BusinessDomain.Region.Actors {
    using System.Threading.Tasks;
    using ActorUtilityTravelTalk.ActorUtility.Messages;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka;
    using Akka.Actor;
    using Akka.Cluster.Sharding;
    using Akka.Event;
    using Commands;
    using DomainUtilities;
    using Events;
    using States;

    public sealed class RegionActor : AbstractEntityActor<RegionState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        public RegionActor() {
            Command<SetLocationInRegion>(c => HandleSetLocation(c));
        }

        public static Task<IActorRef> StartShardRegion(ActorSystem system) {
            return ClusterSharding.Get(system).StartAsync(
                    nameof(RegionActor),
                    Props.Create<RegionActor>(),
                    ClusterShardingSettings.Create(system),
                    new MessageExtractor());
        }

        protected override void UpdateState(IDomainEvent domainEvent) {
            domainEvent?.Match()
                    .With<SetLocationInRegion>(c => UpdateSetLocation(c));
        }

        private void HandleSetLocation(ICommandMessage commandMessage) {
            Emit(new LocationSet(), e => HandleLocationSetEvent(e));
        }

        private void HandleLocationSetEvent(LocationSet @event) {
            Context.GetLogger().Info("YEAH");
        }

        private void UpdateSetLocation(SetLocationInRegion command) {
            
        }
    }
}
