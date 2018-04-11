using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LFT.Selenium.UnitTests
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
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "String" && expectedAttributes["b"]["type"] == "String")), Times.Once);
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
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "RegExp" && expectedAttributes["b"]["type"] == "RegExp")), Times.Once);
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
                It.Is<Dictionary<string, Dictionary<string, string>>>(expectedAttributes => expectedAttributes["a"]["type"] == "String" && expectedAttributes["b"]["type"] == "RegExp")), Times.Once);
        }

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<Dictionary<string, Dictionary<string, string>>>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byAttributes = new ByAttributes(new Dictionary<string, object>());

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

            var byAttributes = new ByAttributes(attributes);

            var resultElement = byAttributes.FindElement(_webElementMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}