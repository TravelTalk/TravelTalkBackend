namespace Commands.CommandHandler {
    using GeneralCommandEvents;

    public sealed class CommandNotSuccessfulResult : ICommandResult {

        private CommandNotSuccessfulResult(CommandResultStatus commandStatus) {
            CommandStatus = commandStatus;
        }

        public static CommandNotSuccessfulResult Create(CommandResultStatus commandStatus) {
            return new CommandNotSuccessfulResult(commandStatus);
        }

        public CommandResultStatus CommandStatus { get; }
    }
}
