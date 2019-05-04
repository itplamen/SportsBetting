namespace SportsBetting.Services.Feeder.Contracts.Factories
{
    using OpenQA.Selenium.Remote;

    public interface IWebDriverFactory
    {
        RemoteWebDriver CreateWebDriver(int port);
    }
}