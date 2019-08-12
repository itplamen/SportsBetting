namespace SportsBetting.Feeder.WinService
{
    using System.ServiceProcess;

    static class Program
    {
        static void Main()
        {
            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new SportsBettingService()
            };
            ServiceBase.Run(ServicesToRun);
        }
    }
}