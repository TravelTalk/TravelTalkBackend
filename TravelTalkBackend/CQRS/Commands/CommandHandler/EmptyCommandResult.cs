namespace Commands.CommandHandler {
    using GeneralCommandEvents;

    public sealed class EmptyCommandResult : ICommandResult {

        public static readonly EmptyCommandResult SuccessfulInstance = new EmptyCommandResult();

        private EmptyCommandResult() {
            CommandStatus = CommandResultStatus.SUCCESSFUL;
        }

        public CommandResultStatus CommandStatus { get; }
    }
}
