namespace BusinessDomain.Region.States {
    using DomainUtilities;

    internal sealed class RegionState : IActorState {
        
        public SpatialExtend SpatialExtend { get; set; }
    }
}
