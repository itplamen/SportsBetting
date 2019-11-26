namespace SportsBetting.Feeder.Core.Contracts.Services
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public interface IWebPagesService<TDriver>
        where TDriver : IWebDriver, IJavaScriptExecutor, IHasSessionId
    {
        bool Load(TDriver webDriver, string url, string waitForElement);

        void ScrollToBottom(TDriver webDriver);
    }
}