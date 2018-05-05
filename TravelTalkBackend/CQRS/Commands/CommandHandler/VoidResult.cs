namespace Commands.CommandHandler {
    using GeneralCommandEvents;

    public sealed class VoidResult : ICommandResult {

        public static readonly VoidResult SuccessfulInstance = new VoidResult();

        private VoidResult() {
            CommandStatus = CommandResultStatus.SUCCESSFUL;
        }

        public CommandResultStatus CommandStatus { get; }
    }
}
