﻿/*! Copyright 2015 - 2021 Open Text. */
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

using System;
using System.Collections.Generic;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LeanFT.Selenium
{
    internal class ByRegex : By
    {
        private readonly Lazy<string> _jsScript = new Lazy<string>(() => InternalUtils.GetScript("GetElementsByRegExp.js"));
        private readonly string _propertyName;
        private readonly string _tagsFilter;
        private readonly Regex _pattern;

        internal ByRegex(string propertyName, Regex pattern, string tagsFilter)
        {
            _propertyName = propertyName;
            _pattern = pattern;
            _tagsFilter = tagsFilter;
        }

        internal ByRegex(string propertyName, Regex pattern) : this(propertyName, pattern, null)
        {
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements;

            var executor = InternalUtils.GetExecutor(context);
            elements = FindElementsByRegex(executor, context as IWebElement);

            Description = string.Format("LFT.Selenium.By.{0}: {1}", _propertyName, _pattern);

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("By." + _propertyName + ": Cannot locate an element using " + ToString());
            }

            return elements[0];
        }

        private ReadOnlyCollection<IWebElement> FindElementsByRegex(IJavaScriptExecutor executor, IWebElement element)
        {
            var executionResult = executor.ExecuteScript(
                _jsScript.Value,
                element,
                _tagsFilter,
                _propertyName,
                _pattern.ToString(),
                InternalUtils.FlagsToString(_pattern));

            var elements = executionResult as ReadOnlyCollection<IWebElement> ?? new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

            return elements;
        }
    }
}
