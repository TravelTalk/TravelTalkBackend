namespace TravelTalk.CommandService.SetLocation {

    public sealed class SetLocationCommand {

        public string UserId { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }
    }
}
