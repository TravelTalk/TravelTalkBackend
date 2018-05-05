namespace TravelTalk.ApplicationService.Tracking.Commands.SetLocation {
    using ActorUtilityTravelTalk.ActorUtility.Sharding;
    using Akka.Actor;
    using Akka.Cluster.Sharding;
    using Akka.Event;
    using BusinessDomain.Region.Actors;
    using BusinessDomain.Region.Commands;
    using BusinessDomain.Region.DomainServices;
    using BusinessDomain.Region.ValueObjects;

    public sealed class SetLocationHandlerActor : ReceiveActor {

        private readonly IActorRef regionShardRegion = ClusterSharding.Get(Context.System).ShardRegion(nameof(RegionActor));

        public SetLocationHandlerActor() {
            Receive<SetLocationCommand>(c => HandleSetLocation(c));
        }

        private void HandleSetLocation(SetLocationCommand command) {
            Position p = new Position(command.Longitude, command.Latitude);
            SetLocationInRegion m = new SetLocationInRegion(MapDistributor.GetAreaOfPosition(p), p, command.UserId, 100);
            regionShardRegion.Tell(ShardEnvelop<object>.Create(m.RegionExtend.Id, m));
            
            Context.GetLogger().Info(command.ToString());
        }
    }
}
