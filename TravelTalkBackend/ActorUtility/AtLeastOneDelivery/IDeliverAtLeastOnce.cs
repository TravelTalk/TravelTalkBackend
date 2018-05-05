namespace ActorUtilityTravelTalk.ActorUtility.AtLeastOneDelivery {
    using Akka.Actor;

    public interface IDeliverAtLeastOnce {

        void DeliverAtOnce(object message, IActorRef target);
    }
}
