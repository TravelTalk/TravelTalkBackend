namespace BusinessDomain.DomainUtilities {
    using System;
    using Akka.Actor;
    using Akka.Persistence;
    using Feitzinger.TyrolSky.Utility.AkkaUtilities.AtLeastOneDelivery;
    using Feitzinger.TyrolSky.Utility.AkkaUtilities.Sharding;
    using Shard;

    /// <summary>
    ///     Base class used by all aggregate root types of actors.
    /// </summary>
    /// <typeparam name="TState"></typeparam>
    public abstract class AbstractEntityActor<TState> : AtLeastOnceDeliveryReceiveActor, IDeliverLeastOnce where TState : IActorState {

        private int eventCount = 0;

        protected AbstractEntityActor() {
            InternalState = new IncludingAtLeastOnceDeliverySnapshot<TState>();

            Recover<SnapshotOffer>(offer => {
                if (offer.Snapshot is IncludingAtLeastOnceDeliverySnapshot<TState> state) {
                    SetDeliverySnapshot(state.AtLeastOnceDeliverySnapshot);
                    InternalState = state;
                }
            });
            Recover<IDomainEvent>(recoveredEvent => UpdateState(recoveredEvent));

            Command<Confirm>(confirmMessage => {
                Persist(new GuaranteedMessageConfirmed(confirmMessage.DeliveryId),
                        x => ConfirmDelivery(x.DeliveryId));
            });

            Command<GuaranteedMessage>(x => {
                Sender.Tell(new Confirm(x.DeliveryId), Self);
                Self.Forward(x.Message);
            });

            Recover<GuaranteedMessageSent>(msgSent => DeliverMessageInternal(msgSent));
            Recover<GuaranteedMessageConfirmed>(msgConfirmed => ConfirmDelivery(msgConfirmed.DeliveryId));
        }

        protected TState State {
            get => InternalState.ActorState;
            set => InternalState.ActorState = value;
        }

        private IncludingAtLeastOnceDeliverySnapshot<TState> InternalState { get; set; }

        public void DeliverAtOnce(object message, IActorRef target) {
            Persist(new GuaranteedMessageSent(message, target), DeliverMessageInternal);
        }

        protected abstract void UpdateState(IDomainEvent domainEvent);

        protected void Emit<TEvent>(TEvent domainEvent, Action<TEvent> handler = null) where TEvent : IDomainEvent {
            Persist(domainEvent, e => {
                UpdateState(e);
                SaveSnapshotIfNecessary();
                handler?.Invoke(e);
            });
        }

        private void DeliverMessageInternal(GuaranteedMessageSent guaranteedMessageSent) {
            Deliver(guaranteedMessageSent.Target.Path, l => {
                if (guaranteedMessageSent.Message is IShardEnvelop envelop) {
                    return new GuaranteeShardEnvelopedMessage(l, envelop);
                }

                return new GuaranteedMessage(l, guaranteedMessageSent.Message);
            });
        }

        private void SaveSnapshotIfNecessary() {
            eventCount = (eventCount + 1) % 20; //todo move constant do configuration
            if (eventCount != 0) {
                return;
            }

            InternalState.AtLeastOnceDeliverySnapshot = GetDeliverySnapshot();
            SaveSnapshot(InternalState);
        }

        private class IncludingAtLeastOnceDeliverySnapshot<TState> where TState : IActorState {
            public TState ActorState { get; set; }

            public AtLeastOnceDeliverySnapshot AtLeastOnceDeliverySnapshot { get; set; }
        }
    }
}
