namespace SportsBetting.Common
{
    using System;

    using SportsBetting.Common.Contracts;
    
    public class ConfigurationManager : IConfigurationManager
    {
        public string Get(string key)
        {
            string configuration = System.Configuration.ConfigurationManager.AppSettings[key];

            if (string.IsNullOrEmpty(configuration))
            {
                throw new InvalidOperationException($"Could not find configuration {key}!");
            }

            return configuration;
        }
    }
}