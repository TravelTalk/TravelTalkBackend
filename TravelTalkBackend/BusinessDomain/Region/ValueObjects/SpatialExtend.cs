namespace BusinessDomain.Region.ValueObjects {
    using ActorUtilityTravelTalk.ActorUtility.Lang.Builder;
    using Newtonsoft.Json;

    public sealed class SpatialExtend {

        public SpatialExtend(Position topLeft, Position bottomRight) {
            TopLeft = topLeft;
            BottomRight = bottomRight;
        }

        public Position TopLeft { get; }

        public Position BottomRight { get; }

        public string Id => $"{GetHashCode()}";

        public bool IsWithin(Position position) {
            return TopLeft.Longitude <= position.Longitude
                    && TopLeft.Latitude <= position.Latitude
                    && BottomRight.Longitude >= position.Longitude
                    && BottomRight.Latitude >= position.Latitude;
        }

        public SpatialExtend AddMargin(double marginInMeters) {
            return new SpatialExtend(TopLeft.Move(-marginInMeters, -marginInMeters), BottomRight.Move(marginInMeters, marginInMeters));
        }

        public override bool Equals(object obj) {
            if (!(obj is SpatialExtend)) {
                return false;
            }

            SpatialExtend other = (SpatialExtend) obj;
            return new EqualsBuilder()
                    .Append(TopLeft, other.TopLeft)
                    .Append(BottomRight, other.BottomRight)
                    .IsEqual;
        }

        public override int GetHashCode() {
            return new HashCodeBuilder()
                    .Append(TopLeft)
                    .Append(BottomRight)
                    .HashCodeValue;
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
