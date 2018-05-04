namespace BusinessDomain.LocationGroup.Actors {
    using DomainUtilities;
    using States;

    internal sealed class ChatRoomActor : AbstractEntityActor<ChatRoomState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        protected override void UpdateState(IDomainEvent domainEvent) {
            // TODO_IMPLEMENT_ME();
        }
    }
}
