namespace BusinessDomain.DomainUtilities {
    using Akka.Actor;

    public interface IDeliverLeastOnce {
        void DeliverAtOnce(object message, IActorRef target);
    }
}
