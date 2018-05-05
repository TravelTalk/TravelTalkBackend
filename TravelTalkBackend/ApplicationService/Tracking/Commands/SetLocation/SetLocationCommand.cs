namespace TravelTalk.ApplicationService.Tracking.Commands {
    using global::Commands.CommandHandler;

    public sealed class SetLocationCommand : AbstractCommand<VoidResult> {

        public SetLocationCommand(string userId, double longitude, double latitude) {
            UserId = userId;
            Longitude = longitude;
            Latitude = latitude;
        }

        public string UserId { get;  }
        
        public double Longitude { get;  }
        
        public double Latitude { get;  }
    }
}
