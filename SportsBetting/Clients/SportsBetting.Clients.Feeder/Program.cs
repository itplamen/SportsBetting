namespace SportsBetting.Clients.Feeder
{
    using System;
    using System.Threading;

    using SportsBetting.Feeder.Bootstrap;

    public class Program
    {
        public static void Main()
        {
            FeederBootstrapper bootstrapper = new FeederBootstrapper();
            bootstrapper.Start();

            Console.WriteLine("Feeder started. Press ESC to stop it...");

            while (Console.ReadKey(true).Key != ConsoleKey.Escape)
            {
                Thread.Sleep(100);
            }

            Console.WriteLine("Stoping the Feeder. Please wait...");
            bootstrapper.Stop();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey(true);
        }
    }
}