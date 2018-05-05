namespace BusinessDomain.Region.States {
    using DomainUtilities;
    using ValueObjects;

    public sealed class RegionState : IActorState {

        public SpatialExtend SpatialExtend { get; set; }
    }
}
