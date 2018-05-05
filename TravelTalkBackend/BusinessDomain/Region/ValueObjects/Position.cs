namespace BusinessDomain.Region.ValueObjects {
    using ActorUtilityTravelTalk.ActorUtility.Lang.Builder;
    using Newtonsoft.Json;

    public sealed class Position {

        public const int LONGITUDES = 360;
        public const int LATITUDE = 180;
        
        public Position(double longitude, double latitude) {
            Longitude = longitude;
            Latitude = latitude;
        }

        public double Longitude { get; }

        public double Latitude { get; }

        public Position Move(double horMeters, double verMeters) {
            return new Position(Longitude + horMeters / 1000, Latitude + verMeters / 1000);
        }
        
        public override bool Equals(object obj) {
            if (!(obj is Position)) {
                return false;
            }

            Position other = (Position) obj;
            return new EqualsBuilder()
                    .Append(Longitude, other.Longitude)
                    .Append(Latitude, other.Latitude)
                    .IsEqual;
        }

        public override int GetHashCode() {
            return new HashCodeBuilder()
                    .Append(Longitude)
                    .Append(Latitude)
                    .HashCodeValue;
        }

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
