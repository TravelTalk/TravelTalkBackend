namespace BusinessDomain.Region.Commands {
    using ActorUtilityTravelTalk.ActorUtility.Messages;
    using ValueObjects;

    public sealed class SetLocationInRegion : ICommandMessage {

        public SetLocationInRegion(SpatialExtend regionExtend, Position position, string userId) {
            RegionExtend = regionExtend;
            Position = position;
            UserId = userId;
        }
        
        public SpatialExtend RegionExtend { get; }

        public Position Position { get; }

        public string UserId { get; }
    }
}
