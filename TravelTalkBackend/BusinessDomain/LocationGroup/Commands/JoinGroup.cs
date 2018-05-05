namespace BusinessDomain.LocationGroup.Commands {
    using ActorUtilityTravelTalk.ActorUtility.Messages;
    using Region.ValueObjects;

    public sealed class JoinGroup : ICommandMessage {
        
        public JoinGroup(string userId, Position position) {
            UserId = userId;
            Position = position;
        }

        public string UserId { get; }

        public Position Position { get; }
    }
}
