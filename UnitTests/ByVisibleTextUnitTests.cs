using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LFT.Selenium.UnitTests
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

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<bool>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byVisibleText = new ByVisibleText(new Regex("text"));

            Assert.Throws<NoSuchElementException>(() => byVisibleText.FindElement(_webElementMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<bool>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>
                {
                    resultWebElementMock1.Object,
                    resultWebElementMock2.Object,
                    resultWebElementMock3.Object
                }));

            var byVisibleText = new ByVisibleText(new Regex("text"));

            var resultElement = byVisibleText.FindElement(_webElementMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}