using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LFT.Selenium.UnitTests
{
    [TestFixture]
    class ByStylesUnitTests
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
        public void FindElements_StringStyles_StylesWithStringTypeSentToScript()
        {
            var styles = new Dictionary<string, object>
            {
                {"a", "1"},
                {"b", "2"}
            };

            var byStyles = new ByStyles(styles);

            byStyles.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedStyles => expectedStyles["a"]["type"] == "String" && expectedStyles["b"]["type"] == "String")), Times.Once);
        }

        [Test]
        public void FindElements_RegExpStyles_StylesWithRegExpTypeSentToScript()
        {
            var styles = new Dictionary<string, object>
            {
                {"a", new Regex("1")},
                {"b", new Regex("2")}
            };

            var byStyles = new ByStyles(styles);

            byStyles.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedStyles => expectedStyles["a"]["type"] == "RegExp" && expectedStyles["b"]["type"] == "RegExp")), Times.Once);
        }

        [Test]
        public void FindElements_StringAndRegExpStyles_StylesWithStringAndRegExpTypesSentToScript()
        {
            var styles = new Dictionary<string, object>
            {
                {"a", "1"},
                {"b", new Regex("2")}
            };

            var byStyles = new ByStyles(styles);

            byStyles.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedStyles => expectedStyles["a"]["type"] == "String" && expectedStyles["b"]["type"] == "RegExp")), Times.Once);
        }

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<Dictionary<string, Dictionary<string, string>>>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byAttributes = new ByStyles(new Dictionary<string, object>());

            Assert.Throws<NoSuchElementException>(() => byAttributes.FindElement(_webElementMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<Dictionary<string, Dictionary<string, string>>>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>
                {
                    resultWebElementMock1.Object,
                    resultWebElementMock2.Object,
                    resultWebElementMock3.Object
                }));

            var attributes = new Dictionary<string, object>
            {
                {"a", "1"},
                {"b", "2"}
            };

            var byAttributes = new ByStyles(attributes);

            var resultElement = byAttributes.FindElement(_webElementMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}