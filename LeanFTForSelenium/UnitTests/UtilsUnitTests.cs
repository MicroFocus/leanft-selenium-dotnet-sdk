using Moq;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Drawing;
using OpenQA.Selenium.Internal;

namespace LeanFTForSelenium.UnitTests
{
    [TestFixture]
    class UtilsUnitTests
    {
        private Mock<IWebElement> _webElementMock;
        private Mock<IJavaScriptExecutor> _javaScriptExecutorMock;
        private Mock<IWrapsDriver> _wrapsDriverMock;
        private Mock<IWebDriver> _webDriverMock;
        private Mock<IOptions> _optionsMock;
        private Mock<IWindow> _windowMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
            _javaScriptExecutorMock = _webElementMock.As<IJavaScriptExecutor>();
            _wrapsDriverMock = _webElementMock.As<IWrapsDriver>();
            _webDriverMock = new Mock<IWebDriver>();
            _wrapsDriverMock.SetupGet(wrapsDrive => wrapsDrive.WrappedDriver).Returns(_webDriverMock.Object);
            _optionsMock = new Mock<IOptions>();
            _webDriverMock.Setup(webDriver => webDriver.Manage()).Returns(_optionsMock.Object);
            _windowMock = new Mock<IWindow>();
            _optionsMock.SetupGet(options => options.Window).Returns(_windowMock.Object);
        }

        [TearDown]
        public void AferEach()
        {
        }

        [Test]
        public void Highlight_ElementIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Utils.Highlight(null));
        }

        [Test]
        public void Highlight_TimeIsNegative_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => Utils.Highlight(_webElementMock.Object, -5));
        }

        [Test]
        public void Highlight_NoTimeProvided_ExecutorExecutesScript_TimeIs3000()
        {
            Utils.Highlight(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object, 3000), Times.Once);
        }

        [Test]
        public void Highlight_SpecificTimeProvided_ExecutorExecutesScript_TimeIsSetToProvided()
        {
            Utils.Highlight(_webElementMock.Object, 5);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object, 5), Times.Once);
        }

        [Test]
        public void Highlight_TimeIsZero_ExecutorDoesNotExecuteScript()
        {
            Utils.Highlight(_webElementMock.Object, 0);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object, It.IsAny<int>()), Times.Never);
        }

        [Test]
        public void ScrollIntoView_ElementIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => Utils.ScrollIntoView(null));
        }

        [Test]
        public void ScrollIntoView_ElementNotVisible_ExecutorExecutesScript()
        {
            _webElementMock.SetupGet(element => element.Size).Returns(new Size(50, 50));
            _webElementMock.SetupGet(element => element.Location).Returns(new Point(600, 0));

            _windowMock.SetupGet(window => window.Size).Returns(new Size(500, 500));
            _windowMock.SetupGet(window => window.Position).Returns(new Point(0, 0));

            Utils.ScrollIntoView(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object), Times.Once);
        }

        [Test]
        public void ScrollIntoView_ElementVisible_ExecutorDoesNotExecuteScript()
        {
            _webElementMock.SetupGet(element => element.Size).Returns(new Size(50, 50));
            _webElementMock.SetupGet(element => element.Location).Returns(new Point(50, 50));

            _windowMock.SetupGet(window => window.Size).Returns(new Size(500, 500));
            _windowMock.SetupGet(window => window.Position).Returns(new Point(0, 0));

            Utils.ScrollIntoView(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object), Times.Never);
        }
    }
}