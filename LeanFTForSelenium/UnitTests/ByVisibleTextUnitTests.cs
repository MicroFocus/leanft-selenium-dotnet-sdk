using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFTForSelenium.UnitTests
{
    [TestFixture]
    class ByVisibleTextUnitTests
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
        public void FindElements_VisibleTextAsString_ScriptIsExecutedWithNonRegexTrue()
        {
            var byVisibleText = new ByVisibleText("text");

            byVisibleText.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), "text", string.Empty, _webElementMock.Object, true), Times.Once);
        }

        [Test]
        public void FindElements_VisibleTextAsRegex_ScriptIsExecutedWithNonRegexFalse()
        {
            var byVisibleText = new ByVisibleText(new Regex("text"));

            byVisibleText.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), "text", string.Empty, _webElementMock.Object, false), Times.Once);
        }
    }
}