namespace Commands.GeneralCommandEvents {
    using Headspring;

    public class CommandResultStatus : Enumeration<CommandResultStatus, string> {

        public static readonly CommandResultStatus SUCCESSFUL = new CommandResultStatus("SUCCESSFUL", true);
        public static readonly CommandResultStatus WARNING = new CommandResultStatus("WARNING", true);
        public static readonly CommandResultStatus DATA_ALREADY_EXISTS = new CommandResultStatus("DATA_ALREADY_EXISTS", false);
        public static readonly CommandResultStatus GENERAL_ERROR = new CommandResultStatus("GENERAL_ERROR", false);

        private CommandResultStatus(string name, bool successfully) : base(name, name) {
            Successfully = successfully;
        }

        public bool Successfully { get; }
    }
}
