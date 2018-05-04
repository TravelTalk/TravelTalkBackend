namespace Commands.GeneralCommandHandling {
    using GeneralCommandEvents;

    public interface ICommand<TCommandResult> where TCommandResult : ICommandResult { }

    public interface ICommandResult {
        CommandResultStatus CommandStatus { get; }
    }

    public sealed class EmptyCommandResult : ICommandResult {

        public static readonly EmptyCommandResult SuccessfulInstance = new EmptyCommandResult();

        private EmptyCommandResult() {
            CommandStatus = CommandResultStatus.SUCCESSFUL;
        }

        public CommandResultStatus CommandStatus { get; }
    }

    public sealed class CommandNotSuccessful : ICommandResult {

        private CommandNotSuccessful(CommandResultStatus commandStatus) {
            CommandStatus = commandStatus;
        }

        public static CommandNotSuccessful CommandNotSuccessfulFactory(CommandResultStatus commandStatus) {
            return new CommandNotSuccessful(commandStatus);
        }

        public CommandResultStatus CommandStatus { get; }
    }
}
