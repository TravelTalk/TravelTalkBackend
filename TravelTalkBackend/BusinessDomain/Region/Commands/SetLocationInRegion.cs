namespace BusinessDomain.Region.Commands {
    using ActorUtilityTravelTalk.ActorUtility.Messages;
    using ValueObjects;

    public sealed class SetLocationInRegion : ICommandMessage {

        public SetLocationInRegion(SpatialExtend regionExtend, Position position, string userId, int margin) {
            RegionExtend = regionExtend;
            Position = position;
            UserId = userId;
            Margin = margin;
        }
        
        public SpatialExtend RegionExtend { get; }

        public Position Position { get; }

        public string UserId { get; }

        public int Margin { get; }
    }
}
