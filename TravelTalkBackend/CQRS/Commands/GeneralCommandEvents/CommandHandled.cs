namespace Commands.GeneralCommandEvents {
    using GeneralCommandHandling;

    public class CommandHandled<TCommand, TCommandResult>
            where TCommand : ICommand<TCommandResult>
            where TCommandResult : ICommandResult {

        private CommandHandled(TCommand command, TCommandResult result, CommandResultStatus resultStatus, string commandProcessingMessage) {
            Command = command;
            Result = result;
            ResultStatus = resultStatus;
            CommandProcessingMessage = commandProcessingMessage;
        }

        public TCommand Command { get; }

        public TCommandResult Result { get; }

        public string CommandProcessingMessage { get; }

        public CommandResultStatus ResultStatus { get; }

        public static CommandHandled<TCommand, TCommandResult> CreateSuccessfulResult(TCommand query, TCommandResult result) {
            return new CommandHandled<TCommand, TCommandResult>(query, result, CommandResultStatus.SUCCESSFUL, "Command was successful handled");
        }

        public static CommandHandled<TCommand, TCommandResult> CreateUnsuccessfulResult(TCommand command) {
            return new CommandHandled<TCommand, TCommandResult>(command, default(TCommandResult), CommandResultStatus.GENERAL_ERROR,
                    "Command not successful handled");
        }

        public static CommandHandled<TCommand, TCommandResult> CreateUnsuccessfulResult(TCommand command, CommandResultStatus resultStatus) {
            return new CommandHandled<TCommand, TCommandResult>(command, default(TCommandResult), resultStatus, "Command not successful handled");
        }

        public static CommandHandled<TCommand, TCommandResult> CreateUnsuccessfulResult(TCommand command, CommandResultStatus resultStatus,
                                                                                        string unsuccessfulMessage) {
            return new CommandHandled<TCommand, TCommandResult>(command, default(TCommandResult), resultStatus, unsuccessfulMessage);
        }
    }

}
