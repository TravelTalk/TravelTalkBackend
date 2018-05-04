namespace BusinessDomain.DomainUtilities {
    using Akka.Actor;

    public static class AtLeastOnceDeliveryExtensions {

        public static void TellGuaranteed(this IActorRef target, object message, IDeliverLeastOnce Sender) {
            Sender.DeliverAtOnce(message, target);
        }
    }
}
