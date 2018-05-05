namespace BusinessDomain {
    using Akka.Actor;
    using LocationGroup.Actors;
    using Region.Actors;
    using User.Actors;

    public static class ShardRegionInitializer {

        public static void Initialize(ActorSystem system) {
            //            LocationGroupActor.StartShardRegion(system);
            RegionActor.StartShardRegion(system);
            //            UserActor.StartShardRegion(system);
        }
    }
}
