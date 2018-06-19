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
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFT.Selenium.UnitTests
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

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<bool>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byVisibleText = new ByVisible(true);

            Assert.Throws<NoSuchElementException>(() => byVisibleText.FindElement(_webElementMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            _javaScriptExecutorMock
                .Setup(javaScriptExecutor => javaScriptExecutor.ExecuteScript(It.IsAny<string>(), It.IsAny<IWebElement>(), It.IsAny<bool>()))
                .Returns(new ReadOnlyCollection<IWebElement>(new List<IWebElement>
                {
                    resultWebElementMock1.Object,
                    resultWebElementMock2.Object,
                    resultWebElementMock3.Object
                }));

            var byVisibleText = new ByVisible(true);

            var resultElement = byVisibleText.FindElement(_webElementMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}