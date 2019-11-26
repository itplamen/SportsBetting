namespace SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs
{
    using System.Collections.ObjectModel;
    using System.Drawing;

    using OpenQA.Selenium;

    public class WebElementStub : IWebElement
    {
        private const string VALID_WAIT_FOR_ELEMENT = "waitForElement";

        private readonly string waitForElement;

        public WebElementStub(string waitForElement)
        {
            this.waitForElement = waitForElement;
        }

        public string TagName { get; private set; }

        public string Text { get; private set; }

        public bool Enabled { get; private set; }

        public bool Selected { get; private set; }

        public Point Location { get; private set; }

        public Size Size { get; private set; }

        public bool Displayed => waitForElement == VALID_WAIT_FOR_ELEMENT;

        public void Clear()
        {
        }

        public void Click()
        {
        }

        public IWebElement FindElement(By by)
        {
            return null;
        }

        public ReadOnlyCollection<IWebElement> FindElements(By by)
        {
            return null;
        }

        public string GetAttribute(string attributeName)
        {
            return string.Empty;
        }

        public string GetCssValue(string propertyName)
        {
            return string.Empty;
        }

        public string GetProperty(string propertyName)
        {
            return string.Empty;
        }

        public void SendKeys(string text)
        {
        }

        public void Submit()
        {
        }
    }
}