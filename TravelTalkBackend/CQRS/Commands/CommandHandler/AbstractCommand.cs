namespace Commands.CommandHandler {
    using Newtonsoft.Json;

    public abstract class AbstractCommand<TCommandResult> : ICommand<TCommandResult>
            where TCommandResult : ICommandResult {

        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }
    }
}
