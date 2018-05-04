namespace Lighthouse {
    using System;

    public class Program {
        public static void Main(string[] args) {
            LighthouseService lighthouseService = new LighthouseService();
            lighthouseService.Start();
            Console.WriteLine("Press Control + C to terminate.");
            Console.CancelKeyPress += (sender, eventArgs) => { lighthouseService.StopAsync().GetAwaiter().GetResult(); };
            lighthouseService.TerminationHandle.Wait();
        }
    }
}
