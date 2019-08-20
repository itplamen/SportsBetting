namespace SportsBetting.Common.Logger
{
    using System;
    using System.IO;
    using System.Text;

    using SportsBetting.Common.Contracts;

    public class Logger : ILogger
    {
        private readonly string file;
        private readonly string directory;

        public Logger(string file, string directory)
        {
            this.file = file;
            this.directory = directory;
        }

        public void LogInfo(string text)
        {
            File.AppendAllText($@"{directory}\{file}", text);
        }

        public void LogError(string text, Exception ex)
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"DateTime: {DateTime.UtcNow}");
            stringBuilder.AppendLine($"Text: {text}");
            stringBuilder.AppendLine($"Exception: {ex.Message}");
            stringBuilder.AppendLine($"InnerException: {ex.InnerException}");
            stringBuilder.AppendLine($"StackTrace: {Environment.StackTrace}");

            File.AppendAllText($@"{directory}\{file}", stringBuilder.ToString());
        }
    }
}