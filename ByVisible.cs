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
using OpenQA.Selenium;

namespace LeanFT.Selenium
{
    internal class ByVisible : By
    {
        private readonly string _jsScript = InternalUtils.GetScript("GetElementsByVisible.js");
        private readonly bool _visible;

        internal ByVisible(bool visible)
        {
            _visible = visible;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var executor = InternalUtils.GetExecutor(context);
            var webElement = context as IWebElement;

            var executionResult = executor.ExecuteScript(
                _jsScript,
                webElement,
                _visible);

            var elements = executionResult as ReadOnlyCollection<IWebElement> ?? new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

            Description = string.Format("LFT.Selenium.By.Visible: {0}", _visible);

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("By.Visible: Cannot locate an element using " + ToString());
            }

            return elements[0];
        }
    }
}