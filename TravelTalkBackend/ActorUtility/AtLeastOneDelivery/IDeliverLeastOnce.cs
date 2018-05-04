namespace ActorUtilityTravelTalk.ActorUtility.AtLeastOneDelivery {
    using Akka.Actor;

    public interface IDeliverLeastOnce {

        void DeliverAtOnce(object message, IActorRef target);
    }
}
