namespace TravelTalk.CommandServiceHost {
    using System;

    internal class Program {

        private static void Main(string[] args) {
            Console.WriteLine("Hello Command Service!");

            CommandService commandService = new CommandService();
            commandService.Start();

            Console.CancelKeyPress += (sender, eventArgs) => {
                Console.WriteLine("Leaving cluster ... ciao bella!");
                commandService.CoordinatedShutdown().GetAwaiter().GetResult();
            };
            commandService.TerminationHandle.Wait();
        }
    }
}
