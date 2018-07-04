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
using System.Text;
using OpenQA.Selenium;

namespace LeanFT.Selenium
{
    /// <summary>
    /// A locator that locates elements that match one or more of the specified locators (attributes, tags, styles etc.).
    /// </summary>
    public class ByAny : By
    {
        private readonly OpenQA.Selenium.By[] _bys;

        /// <summary>
        /// A constructor for the ByAny locator.
        /// </summary>
        /// <param name="bys">The locators (Bys) by which the elements should be identified.</param>
        public ByAny(params OpenQA.Selenium.By[] bys)
        {
            _bys = bys;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var elements = new List<IWebElement>();

            foreach (var by in _bys)
            {
                elements.AddRange(by.FindElements(context));
            }

            return new ReadOnlyCollection<IWebElement>(elements);
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("ByAny: Cannot locate an element using " + ToString());
            }

            return elements[0];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("ByAny(");

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