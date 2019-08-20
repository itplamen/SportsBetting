namespace SportsBetting.Common.Logger
{
    using System;
    using System.IO;

    using SportsBetting.Common.Constants;
    using SportsBetting.Common.Contracts;

    public class LoggerFactory : ILoggerFactory
    {
        private readonly IConfigurationManager configurationManager;

        public LoggerFactory(IConfigurationManager configurationManager)
        {
            this.configurationManager = configurationManager;
        }

        public ILogger Create(string file, string directory)
        {
            string dateTime = DateTime.UtcNow.ToString("dd-MM-yyyy");
            string fullDirectoryPath = CreateDirectory($@"{directory}\{dateTime}");

            return new Logger(file, fullDirectoryPath);
        }

        private string CreateDirectory(string directory)
        {
            string logsRootDirectory = configurationManager.Get(ConfigConstants.LOGS_DIRECTORY);
            string fullDirectoryPath = $@"{logsRootDirectory}\{directory}";

            if (!Directory.Exists(fullDirectoryPath))
            {
                Directory.CreateDirectory(fullDirectoryPath);
            }

            return fullDirectoryPath;
        }
    }
}