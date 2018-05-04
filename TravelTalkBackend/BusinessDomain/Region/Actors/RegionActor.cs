﻿namespace BusinessDomain.Region.Actors {
    using DomainUtilities;
    using States;

    internal sealed class RegionActor : AbstractEntityActor<RegionState> {

        public override string PersistenceId => $"{Context.Parent.Path.Name}-{Self.Path.Name}";

        protected override void UpdateState(IDomainEvent domainEvent) {
            // TODO_IMPLEMENT_ME();
        }
    }
}
