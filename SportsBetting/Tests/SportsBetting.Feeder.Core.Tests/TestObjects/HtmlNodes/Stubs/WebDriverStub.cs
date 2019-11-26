namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;

    public class WebDriverStub : IWebDriver, IJavaScriptExecutor, IHasSessionId
    {
        private readonly List<string> windows;

        public WebDriverStub()
        {
            Url = "TestUrl";
            windows = new List<string>();
        }

        public string Url { get; set; }

        public string Title { get; private set; }

        public string PageSource { get; private set; }

        public string CurrentWindowHandle { get; private set; }

        public ReadOnlyCollection<string> WindowHandles => new ReadOnlyCollection<string>(windows);

        public SessionId SessionId { get; private set; }

        public void Close()
        {
            windows.RemoveAt(windows.Count - 1);
        }

        public void Dispose()
        {
        }

        public object ExecuteAsyncScript(string script, params object[] args)
        {
            return new object();
        }

        public object ExecuteScript(string script, params object[] args)
        {
            windows.Add("Page");

            return new object();
        }

        public IWebElement FindElement(By by)
        {
            string classNameToFind = "waitForElement";
            By waitForElement = By.ClassName(classNameToFind);

            if (by.Equals(waitForElement))
            {
                return new WebElementStub(classNameToFind);
            }

            return new WebElementStub("invalid");
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return null;
        }

        public IOptions Manage()
        {
            return null;
        }

        public INavigation Navigate()
        {
            return new NavigationStub();
        }

        public void Quit()
        {
        }

        public ITargetLocator SwitchTo()
        {
            throw new NotImplementedException();
        }
    }
}