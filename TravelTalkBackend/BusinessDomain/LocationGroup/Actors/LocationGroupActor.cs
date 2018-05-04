namespace BusinessDomain.LocationGroup.Actors {
    using Akka;
    using DomainUtilities;
    using States;

    internal sealed class LocationGroupActor : AbstractEntityActor<LocationGroupState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        protected override void UpdateState(IDomainEvent domainEvent) {
//            domainEvent.Match()
//                    .With<>()
        }
    }
}
