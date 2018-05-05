namespace Commands.CommandHandler {
    using GeneralCommandEvents;

    public interface ICommandResult {
        CommandResultStatus CommandStatus { get; }
    }
}