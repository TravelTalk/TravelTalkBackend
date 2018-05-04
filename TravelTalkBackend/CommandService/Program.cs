using System;


namespace CommandService {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello Command Service!");
            
            
            DomainHostingService domainHostingService = new DomainHostingService();
            domainHostingService.Start();

            Console.CancelKeyPress += (sender, eventArgs) => domainHostingService.StopAsync().GetAwaiter().GetResult();
            domainHostingService.TerminationHandle.Wait();
        }
    }
}
