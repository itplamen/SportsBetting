namespace SportsBetting.Feeder.Core.Tests.ServicesTests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using SportsBetting.Feeder.Core.Contracts.Services;
    using SportsBetting.Feeder.Core.Services;
    using SportsBetting.Feeder.Core.Tests.TestObjects.HtmlNodes.Stubs;

    [TestClass]
    public class WebPagesServiceTests
    {
        private WebDriverStub webDriver;
        private IWebPagesService<WebDriverStub> webPagesService;

        [TestInitialize]
        public void TestInitialize()
        {
            webDriver = new WebDriverStub();
            webPagesService = new WebPagesService<WebDriverStub>();
        }

        [TestMethod]
        public void LoadShouldReturnFalseWhenUrlAndWaitForElementAreNull()
        {
            bool isLoaded = webPagesService.Load(webDriver, null, null);

            Assert.IsFalse(isLoaded);
        }

        [TestMethod]
        public void LoadShouldReturnFalseWhenWaitForElementIsInvalid()
        {
            bool isLoaded = webPagesService.Load(webDriver, "url", "invalidWaitForElement");

            Assert.IsFalse(isLoaded);
        }

        [TestMethod]
        public void LoadShouldReturnTrueWhenWaitForElementIsValid()
        {
            bool isLoaded = webPagesService.Load(webDriver, "url", "waitForElement");

            Assert.IsTrue(isLoaded);
        }

        [TestMethod]
        public void LoadShouldReturnTrueWhenWaitForElementIsInvalidButUrlIsValid()
        {
            bool isLoaded = webPagesService.Load(webDriver, "TestUrl", "invalidWaitForElement");

            Assert.IsTrue(isLoaded);
        }

        [TestMethod]
        public void ScrollToBottomShouldHasAtleastOneHandledWindows()
        {
            webPagesService.ScrollToBottom(webDriver);

            Assert.IsTrue(webDriver.WindowHandles.Any());
        }
    }
}