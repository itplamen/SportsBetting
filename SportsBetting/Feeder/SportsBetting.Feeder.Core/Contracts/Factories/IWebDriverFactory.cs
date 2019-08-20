namespace SportsBetting.Feeder.Core.Contracts.Factories
{
    using OpenQA.Selenium.Remote;

    public interface IWebDriverFactory
    {
        RemoteWebDriver CreateWebDriver(int port);
    }
}