namespace TravelTalk.CommandService {
    using System;

    class Program {
        
        static void Main(string[] args) {
            Console.WriteLine("Hello Command Service!");
            
            CommandService commandService = new CommandService();
            commandService.Start();

            Console.CancelKeyPress += (sender, eventArgs) => commandService.CoordinatedShutdown().GetAwaiter().GetResult();
            commandService.TerminationHandle.Wait();
        }
    }
}
