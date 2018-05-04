namespace TravelTalk.CommandService.SetLocation {
    using Commands.CommandHandler;

    public sealed class SetLocationCommand : ICommand<EmptyCommandResult> {

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
