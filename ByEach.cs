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
using System.Linq;
using System.Text;
using OpenQA.Selenium;

namespace LeanFT.Selenium
{
    /// <summary>
    /// A locator that locates elements that match all specified locators (attributes, tags, styles etc.).
    /// </summary>
    public class ByEach : By
    {
        private readonly OpenQA.Selenium.By[] _bys;

        /// <summary>
        /// A constructor for the ByEach locator.
        /// </summary>
        /// <param name="bys">The locators (Bys) by which the elements should be identified.</param>
        public ByEach(params OpenQA.Selenium.By[] bys)
        {
            _bys = bys;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            if (_bys.Length == 1)
            {
                return _bys[0].FindElements(context);
            }

            var listOfByCollections = _bys.Select(by => by.FindElements(context)).ToList();

            var intersection = new List<IWebElement>(listOfByCollections[0]);
            for (var i = 1; i < listOfByCollections.Count; i++)
            {
                intersection = new List<IWebElement>(intersection.Intersect(listOfByCollections[i]));
            }

            return new ReadOnlyCollection<IWebElement>(intersection);
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("ByEach: Cannot locate an element using " + ToString());
            }

            return elements[0];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("ByEach(");

            for (var i = 0; i < _bys.Length; i++)
            {
                stringBuilder.Append(_bys[i]);
                if (i != _bys.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}