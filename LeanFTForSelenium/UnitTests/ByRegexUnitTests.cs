using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFTForSelenium.UnitTests
{
    [TestFixture]
    class ByRegexUnitTests
    {
        [Test]
        public void FindElements_ContextIsWebElement_ScriptIsExecutedWithElement()
        {
            var webElementMock = new Mock<IWebElement>();
            var javaScriptExecutorMock = webElementMock.As<IJavaScriptExecutor>();

            const string propertyName = "propertyName";
            const string tagsFilter = "a";
            var regex = new Regex("Pattern");

            var byRegex = new ByRegex(propertyName, regex, tagsFilter);

            byRegex.FindElements(webElementMock.Object);

            javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), webElementMock.Object, tagsFilter, propertyName, "Pattern", string.Empty), Times.Once);
        }

        [Test]
        public void FindElements_ContextIsNotWebElement_ScriptIsExecutedWithNullAsElement()
        {
            var searchContextMock = new Mock<ISearchContext>();
            var javaScriptExecutorMock = searchContextMock.As<IJavaScriptExecutor>();

            const string propertyName = "propertyName";
            const string tagsFilter = "a";
            var regex = new Regex("Pattern");

            var byRegex = new ByRegex(propertyName, regex, tagsFilter);

            byRegex.FindElements(searchContextMock.Object);

            javaScriptExecutorMock.Verify(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), null, tagsFilter, propertyName, "Pattern", string.Empty), Times.Once);
        }
    }
}