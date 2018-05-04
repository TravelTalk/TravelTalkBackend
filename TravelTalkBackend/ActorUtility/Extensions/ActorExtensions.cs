namespace ActorUtilityTravelTalk.ActorUtility.Extensions {
    using Akka.Actor;

    public static class ActorExtensions {

        public static void CommitSuicide(this IActorRef actorRef) {
            actorRef.Tell(PoisonPill.Instance);
        }
    }
}
