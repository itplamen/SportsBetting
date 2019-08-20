namespace SportsBetting.Common.Contracts
{
    using System;

    public interface ILogger
    {
        void LogInfo(string text);

        void LogError(string text, Exception ex);
    }
}