namespace BusinessDomain.Region.States {
    using DomainUtilities;

    public sealed class RegionState : IActorState {

        public SpatialExtend SpatialExtend { get; set; }
    }
}
