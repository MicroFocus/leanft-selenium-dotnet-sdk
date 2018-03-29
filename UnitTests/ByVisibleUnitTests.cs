using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LFT.Selenium.UnitTests
{
    [TestFixture]
    class ByVisibleUnitTests
    {
        private Mock<IWebElement> _webElementMock;
        private Mock<IJavaScriptExecutor> _javaScriptExecutorMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
            _javaScriptExecutorMock = _webElementMock.As<IJavaScriptExecutor>();
        }

        [Test]
        public void FindElements_ScriptIsExecuted()
        {
            var byVisible = new ByVisible(true);

            byVisible.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), _webElementMock.Object, true), Times.Once);
        }
    }
}