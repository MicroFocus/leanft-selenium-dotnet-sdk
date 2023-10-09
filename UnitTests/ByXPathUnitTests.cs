/*! Copyright 2015 - 2021 Open Text. */
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

using Moq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace LeanFT.Selenium.UnitTests
{
    [TestFixture]
    class ByXPathUnitTests
    {
        private Mock<IWebElement> _webElementMock;
        private Mock<ISearchContext> _findsByXPathMock;

        [SetUp]
        public void BeforeEach()
        {
            _webElementMock = new Mock<IWebElement>();
            _findsByXPathMock = _webElementMock.As<ISearchContext>();
        }

        [Test]
        public void FindElements_FindElementsByXPathIsCalledWithXPath()
        {
            const string attributeName = "attr";
            const string xPath = "xpath";

            var byXPath = new ByXPath(attributeName, xPath);

            byXPath.FindElements(_webElementMock.Object);

            _findsByXPathMock.Verify(findsByXPath => findsByXPath.FindElements(By.XPath(".//*[@attr = 'xpath']")), Times.Once);
        }

        [Test]
        public void FindElement_FindElementByXPathIsCalledWithXPath()
        {
            const string attributeName = "attr";
            const string xPath = "xpath";

            var byXPath = new ByXPath(attributeName, xPath);

            byXPath.FindElement(_webElementMock.Object);

            _findsByXPathMock.Verify(findsByXPath => findsByXPath.FindElement(By.XPath(".//*[@attr = 'xpath']")), Times.Once);
        }
    }
}
