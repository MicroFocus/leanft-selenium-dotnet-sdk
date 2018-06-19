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
    class ByEachUnitTests
    {
        private Mock<IWebElement> _webElementMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
        }

        [Test]
        public void FindElements_SingleBy_ShouldCallFindElementsOfAllTheBy()
        {
            var byMock = new Mock<OpenQA.Selenium.By>();

            var byEach = new ByEach(byMock.Object);

            byEach.FindElements(_webElementMock.Object);

            byMock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
        }

        [Test]
        public void FindElements_MoreThanOneBy_ShouldCallToFindElementsOfAllBys()
        {
            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byEach = new ByAny(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            byEach.FindElements(_webElementMock.Object);

            by1Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
            by2Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
            by2Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
        }

        [Test]
        public void FindElements_ShouldReturnTheIntersectionOfAllBysFindElementsResults()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            var result1List = new List<IWebElement>() { resultWebElementMock1.Object, resultWebElementMock2.Object };
            var result2List = new List<IWebElement>() { resultWebElementMock3.Object, resultWebElementMock2.Object, resultWebElementMock1.Object };
            var result3List = new List<IWebElement>() { resultWebElementMock2.Object };

            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result1List));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result2List));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result3List));

            var byEach = new ByEach(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            var resultElements = byEach.FindElements(_webElementMock.Object);

            Assert.AreEqual(1, resultElements.Count);
            Assert.AreSame(resultWebElementMock2.Object, resultElements[0]);
        }

        [Test]
        public void FindElement_ShouldThrowExceptionIfNoElementsFound()
        {
            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(new List<IWebElement>()));

            var byEach = new ByEach(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            Assert.Throws<NoSuchElementException>(() => byEach.FindElement(_webElementMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var resultWebElementMock2 = new Mock<IWebElement>();
            var resultWebElementMock3 = new Mock<IWebElement>();

            var result1List = new List<IWebElement>() { resultWebElementMock1.Object, resultWebElementMock2.Object };
            var result2List = new List<IWebElement>() { resultWebElementMock3.Object, resultWebElementMock2.Object, resultWebElementMock1.Object };
            var result3List = new List<IWebElement>() { resultWebElementMock2.Object, resultWebElementMock1.Object };

            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result1List));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result2List));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result3List));

            var byEach = new ByEach(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            var resultElement = byEach.FindElement(_webElementMock.Object);

            Assert.AreSame(resultWebElementMock1.Object, resultElement);
        }
    }
}