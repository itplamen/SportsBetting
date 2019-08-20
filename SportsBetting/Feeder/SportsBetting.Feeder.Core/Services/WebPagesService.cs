namespace SportsBetting.Feeder.Core.Services
{
    using System;
    using System.Threading;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;

    using SportsBetting.Feeder.Core.Contracts.Services;

    public class WebPagesService : IWebPagesService
    {
        private const double PAGE_LOAD_TIMEOUT = 7;
        private const int PAGE_LOAD_MAX_RETRIES = 3;

        public bool Load(RemoteWebDriver webDriver, string url, string waitForElement)
        {
            if (webDriver.Url == url)
            {
                return true;
            }

            webDriver.Navigate().GoToUrl(url);

            return EnsureIsLoaded(webDriver, waitForElement);
        }

        public void ScrollToBottom(RemoteWebDriver webDriver)
        {
            string currentUrl = string.Empty;

            while (webDriver.Url != currentUrl)
            {
                currentUrl = webDriver.Url;

                webDriver.ExecuteScript($"window.scrollTo(0, document.body.scrollHeight);");
                Thread.Sleep(4000);
            }
        }

        private bool EnsureIsLoaded(RemoteWebDriver webDriver, string waitForElement)
        {
            int retries = 1;

            while (retries <= PAGE_LOAD_MAX_RETRIES)
            {
                if (IsLoaded(webDriver, waitForElement))
                {
                    return true;
                }

                webDriver.Navigate().Refresh();
                retries++;
            }

            return false;
        }

        private bool IsLoaded(RemoteWebDriver webDriver, string waitForElement)
        {
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(PAGE_LOAD_TIMEOUT));
            IWebElement webElement = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(waitForElement)));

            return webElement.Displayed;
        }
    }
}