using System.Collections.Generic;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFTForSelenium.UnitTests
{
    [TestFixture]
    class ByAttributesUnitTests
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
        public void FindElements_StringAttributes_AttributesWithStringTypeSentToScript()
        {
            var attributes = new Dictionary<string, object>
            {
                {"a", "1"},
                {"b", "2"}
            };

            var byAttributes = new ByAttributes(attributes);

            byAttributes.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<IDictionary<string, IDictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "String" && expectedAttributes["b"]["type"] == "String")), Times.Once);
        }

        [Test]
        public void FindElements_RegExpAttributes_AttributesWithRegExpTypeSentToScript()
        {
            var attributes = new Dictionary<string, object>
            {
                {"a", new Regex("1")},
                {"b", new Regex("2")}
            };

            var byAttributes = new ByAttributes(attributes);

            byAttributes.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<IDictionary<string, IDictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "RegExp" && expectedAttributes["b"]["type"] == "RegExp")), Times.Once);
        }

        [Test]
        public void FindElements_StringAndRegExpAttributes_AttributesWithStringAndRegExpTypesSentToScript()
        {
            var attributes = new Dictionary<string, object>
            {
                {"a", "1"},
                {"b", new Regex("2")}
            };

            var byAttributes = new ByAttributes(attributes);

            byAttributes.FindElements(_webElementMock.Object);

            _javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                _webElementMock.Object,
                It.Is<IDictionary<string, IDictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "String" && expectedAttributes["b"]["type"] == "RegExp")), Times.Once);
        }
    }
}