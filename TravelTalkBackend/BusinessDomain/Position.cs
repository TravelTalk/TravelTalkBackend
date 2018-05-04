namespace BusinessDomain {

    public sealed class Position {

        public Position(double longitute, double latitude) {
            Longitute = longitute;
            Latitude = latitude;

        }

        public double Longitute { get; }

        public double Latitude { get; }
    }
}
