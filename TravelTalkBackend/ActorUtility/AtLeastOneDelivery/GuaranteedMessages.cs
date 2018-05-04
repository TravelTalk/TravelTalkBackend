namespace Feitzinger.TyrolSky.Utility.AkkaUtilities.AtLeastOneDelivery {
    using Akka.Actor;

    public class GuaranteedMessage {
        public GuaranteedMessage(long deliveryId, object message) {
            DeliveryId = deliveryId;
            Message = message;
        }

        public long DeliveryId { get; }

        public object Message { get; }
    }

    public class Confirm {
        public Confirm(long deliveryId) {
            DeliveryId = deliveryId;
        }

        public long DeliveryId { get; }
    }

    public interface IEvent { }

    public class GuaranteedMessageSent : IEvent {
        public GuaranteedMessageSent(object message, IActorRef target) {
            Message = message;
            Target = target;
        }

        public object Message { get; }

        public IActorRef Target { get; }
    }

    public class GuaranteedMessageConfirmed : IEvent {
        public GuaranteedMessageConfirmed(long deliveryId) {
            DeliveryId = deliveryId;
        }

        public long DeliveryId { get; }
    }

}
