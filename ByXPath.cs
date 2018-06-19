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

using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace LeanFT.Selenium
{
    internal class ByXPath : By
    {
        private readonly string _attributeName;
        private readonly string _xPath;

        public ByXPath(string attributeName, string xPath)
        {
            _attributeName = attributeName;
            _xPath = xPath;
        }

        public override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementsByXPath(string.Format(".//*[@{0} = '{1}']", _attributeName, _xPath));
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementByXPath(string.Format(".//*[@{0} = '{1}']", _attributeName, _xPath));
        }
    }
}