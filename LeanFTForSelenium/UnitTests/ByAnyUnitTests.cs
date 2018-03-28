﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFTForSelenium.UnitTests
{
    [TestFixture]
    class ByAnyUnitTests
    {
        private Mock<IWebElement> _webElementMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
        }

        [Test]
        public void FindElements_ShouldCallFindElementsOfAllBys()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var result1List = new List<IWebElement>() {resultWebElementMock1.Object};
            var resultWebElementMock2 = new Mock<IWebElement>();
            var result2List = new List<IWebElement>() { resultWebElementMock2.Object };
            var resultWebElementMock3 = new Mock<IWebElement>();
            var result3List = new List<IWebElement>() { resultWebElementMock3.Object };

            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result1List));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result2List));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result3List));

            var byAny = new ByAny(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            byAny.FindElements(_webElementMock.Object);

            by1Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
            by2Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
            by2Mock.Verify(by => by.FindElements(_webElementMock.Object), Times.Once);
        }

        [Test]
        public void FindElements_ShouldReturnTheResultOfAllBysFindElements()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var result1List = new List<IWebElement>() { resultWebElementMock1.Object };
            var resultWebElementMock2 = new Mock<IWebElement>();
            var result2List = new List<IWebElement>() { resultWebElementMock2.Object };
            var resultWebElementMock3 = new Mock<IWebElement>();
            var result3List = new List<IWebElement>() { resultWebElementMock3.Object };

            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result1List));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result2List));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result3List));

            var byAny = new ByAny(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            var resultElements = byAny.FindElements(_webElementMock.Object);

            Assert.AreEqual(3, resultElements.Count);
            Assert.True(resultElements.Contains(resultWebElementMock1.Object));
            Assert.True(resultElements.Contains(resultWebElementMock2.Object));
            Assert.True(resultElements.Contains(resultWebElementMock3.Object));
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

            var byAny = new ByAny(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            Assert.Throws<NoSuchElementException>(() => byAny.FindElement(_webElementMock.Object));
        }

        [Test]
        public void FindElement_ShouldReturnFirstFoundElement()
        {
            var resultWebElementMock1 = new Mock<IWebElement>();
            var result1List = new List<IWebElement>() { resultWebElementMock1.Object };
            var resultWebElementMock2 = new Mock<IWebElement>();
            var result2List = new List<IWebElement>() { resultWebElementMock2.Object };
            var resultWebElementMock3 = new Mock<IWebElement>();
            var result3List = new List<IWebElement>() { resultWebElementMock3.Object };

            var by1Mock = new Mock<OpenQA.Selenium.By>();
            by1Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result1List));

            var by2Mock = new Mock<OpenQA.Selenium.By>();
            by2Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result2List));

            var by3Mock = new Mock<OpenQA.Selenium.By>();
            by3Mock.Setup(by => by.FindElements(It.IsAny<ISearchContext>())).Returns(() => new ReadOnlyCollection<IWebElement>(result3List));

            var byAny = new ByAny(by1Mock.Object, by2Mock.Object, by3Mock.Object);

            var resultElement = byAny.FindElement(_webElementMock.Object);

            Assert.AreSame(resultElement, resultWebElementMock1.Object);
        }
    }
}