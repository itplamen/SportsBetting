namespace SportsBetting.Services.Feeder.Contracts.Services
{
    using OpenQA.Selenium.Remote;

    public interface IWebPagesService
    {
        bool Load(RemoteWebDriver webDriver, string url, string waitForElement);

        void ScrollToBottom(RemoteWebDriver webDriver);
    }
}