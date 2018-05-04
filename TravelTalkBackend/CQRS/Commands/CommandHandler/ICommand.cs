namespace Commands.CommandHandler {
    using GeneralCommandEvents;

    public interface ICommand<TCommandResult> where TCommandResult : ICommandResult { }

    public interface ICommandResult {
        CommandResultStatus CommandStatus { get; }
    }


}
