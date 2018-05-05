namespace Commands.CommandHandler {

    public interface ICommand<TCommandResult> where TCommandResult : ICommandResult { }
}
