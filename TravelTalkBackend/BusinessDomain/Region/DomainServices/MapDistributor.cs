namespace BusinessDomain.Region.DomainServices {
    using ValueObjects;

    public static class MapDistributor {

        public static SpatialExtend GetAreaOfPosition(Position position) {
            return new SpatialExtend(new Position(-180, -90), new Position(180, 90));
        }
    }
}
