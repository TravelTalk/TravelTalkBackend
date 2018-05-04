namespace BusinessDomain.LocationGroup.States {
    using DomainUtilities;

    internal sealed class LocationGroupState : IActorState {

        public int UserCount { get; set; }

        public SpatialExtend SpatialExtend { get; set; }
    }
}
