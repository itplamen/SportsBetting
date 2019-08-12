namespace SportsBetting.Feeder.WinService
{
    using System.ServiceProcess;

    using SportsBetting.Feeder.Bootstrap;

    public partial class SportsBettingService : ServiceBase
    {
        private readonly FeederBootstrapper bootstrapper;

        public SportsBettingService()
        {
            InitializeComponent();

            bootstrapper = new FeederBootstrapper();
        }

        protected override void OnStart(string[] args)
        {
            bootstrapper.Start();
        }

        protected override void OnStop()
        {
            bootstrapper.Stop();
        }

        protected override void OnShutdown()
        {
            this.OnStop();
        }
    }
}