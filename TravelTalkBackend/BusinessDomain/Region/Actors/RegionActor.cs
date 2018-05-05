namespace BusinessDomain.Region.Actors {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using ActorUtilityTravelTalk.ActorUtility.Messages;
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka;
    using Akka.Actor;
    using Akka.Cluster.Sharding;
    using Akka.Event;
    using Akka.Util.Internal;
    using Commands;
    using DomainUtilities;
    using Events;
    using LocationGroup.Actors;
    using LocationGroup.Commands;
    using States;
    using ValueObjects;

    public sealed class RegionActor : AbstractEntityActor<RegionState> {

        private readonly IDictionary<SpatialExtend, Guid> locationGroupMap = new Dictionary<SpatialExtend, Guid>();
        private readonly IActorRef locationGroupShardRegion = ClusterSharding.Get(Context.System).ShardRegion(nameof(LocationGroupActor));

        public RegionActor() {
            Command<SetLocationInRegion>(c => HandleSetLocation(c));
        }
        
        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

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

        private void HandleSetLocation(SetLocationInRegion command) {
            IList<KeyValuePair<SpatialExtend, Guid>> matchingGroups = locationGroupMap
                    .Where(kvp => kvp.Key.AddMargin(command.Margin).IsWithin(command.Position))
                    .ToList();

            foreach (KeyValuePair<SpatialExtend, Guid> group in matchingGroups) {
                locationGroupShardRegion.Tell(ShardEnvelop<JoinGroup>.Create(
                        group.Value.ToString(), new JoinGroup(command.UserId, command.Position)));
            }

            if (!matchingGroups.Any()) {
                locationGroupMap.Put(new SpatialExtend(command.Position, command.Position), Guid.NewGuid());
            }
            
            Emit(new LocationSet(), e => HandleLocationSetEvent(e));
        }

        private void HandleLocationSetEvent(LocationSet @event) {
            Context.GetLogger().Info("YEAH");
        }

        private void UpdateSetLocation(SetLocationInRegion command) {
            
        }
    }
}
