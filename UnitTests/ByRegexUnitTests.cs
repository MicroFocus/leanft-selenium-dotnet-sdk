/*! (c) Copyright 2015 - 2018 Micro Focus or one of its affiliates. */
//
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Apache License 2.0 - Apache Software Foundation
// www.apache.org
// Apache License Version 2.0, January 2004 http://www.apache.org/licenses/ TERMS AND CONDITIONS FOR USE, REPRODUCTION ...
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFT.Selenium.UnitTests
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

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            var searchContextMock = new Mock<ISearchContext>();
            var javaScriptExecutorMock = searchContextMock.As<IJavaScriptExecutor>();

            javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                    It.IsAny<IWebElement>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byRegex = new ByRegex("a", new Regex("Pattern"), "a");

            Assert.Throws<NoSuchElementException>(() => byRegex.FindElement(searchContextMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var searchContextMock = new Mock<ISearchContext>();
            var javaScriptExecutorMock = searchContextMock.As<IJavaScriptExecutor>();

            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(),
                    It.IsAny<IWebElement>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(),
                    It.IsAny<string>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>
                {
                    resultWebElementMock1.Object,
                    resultWebElementMock2.Object,
                    resultWebElementMock3.Object
                }));

            var byRegex = new ByRegex("a", new Regex("Pattern"), "a");

            var resultElement = byRegex.FindElement(searchContextMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}