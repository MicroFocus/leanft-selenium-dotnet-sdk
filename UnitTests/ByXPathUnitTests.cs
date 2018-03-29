using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace LFT.Selenium.UnitTests
{
    [TestFixture]
    class ByXPathUnitTests
    {
        private Mock<IWebElement> _webElementMock;
        private Mock<IFindsByXPath> _findsByXPathMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
            _findsByXPathMock = _webElementMock.As<IFindsByXPath>();
        }

        [Test]
        public void FindElements_FindElementsByXPathIsCalledWithXPath()
        {
            const string attributeName = "attr";
            const string xPath = "xpath";

            var byXPath = new ByXPath(attributeName, xPath);

            byXPath.FindElements(_webElementMock.Object);

            _findsByXPathMock.Verify(findsByXPath => findsByXPath.FindElementsByXPath(".//*[@attr = 'xpath']"), Times.Once);
        }

        [Test]
        public void FindElement_FindElementByXPathIsCalledWithXPath()
        {
            const string attributeName = "attr";
            const string xPath = "xpath";

            var byXPath = new ByXPath(attributeName, xPath);

            byXPath.FindElement(_webElementMock.Object);

            _findsByXPathMock.Verify(findsByXPath => findsByXPath.FindElementByXPath(".//*[@attr = 'xpath']"), Times.Once);
        }
    }
}