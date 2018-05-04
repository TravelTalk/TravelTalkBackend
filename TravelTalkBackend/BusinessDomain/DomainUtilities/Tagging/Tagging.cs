namespace BusinessDomain.DomainUtilities.Tagging {
    using System.Collections.Immutable;
    using Akka.Persistence.Journal;

    public class TagAdapter : IWriteEventAdapter {

        public string Manifest(object evt) {
            return string.Empty;
        }

        public object ToJournal(object evt) {
            ITaggedEvent taggedEvent = evt as ITaggedEvent;
            if (evt == null) {
                return evt;
            }

            return WithTag(taggedEvent, taggedEvent.EventTag);
        }

        internal Tagged WithTag(object evt, string tag) {
            return new Tagged(evt, ImmutableHashSet.Create(tag));
        }
    }
}
