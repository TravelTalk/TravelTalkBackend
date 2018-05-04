namespace ActorUtilityTravelTalk.ActorUtility.Extensions {
    using Akka.Actor;
    using AtLeastOneDelivery;

    public static class ActorExtensions {

        public static void CommitSuicide(this IActorRef actorRef) {
            actorRef.Tell(PoisonPill.Instance);
        }

        public static void TellGuaranteed(this IActorRef target, object message, IDeliverLeastOnce sender) {
            sender.DeliverAtOnce(message, target);
        }
    }
}
