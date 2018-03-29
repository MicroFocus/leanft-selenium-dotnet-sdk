using System.Collections.Generic;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFTForSelenium.UnitTests
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

            var byStyles = new ByAttributes(styles);

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

            var byStyles = new ByAttributes(styles);

            byStyles.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedStyles => expectedStyles["a"]["type"] == "String" && expectedStyles["b"]["type"] == "RegExp")), Times.Once);
        }
    }
}