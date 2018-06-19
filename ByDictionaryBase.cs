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

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace LeanFT.Selenium
{
    internal abstract class ByDictionaryBase : By
    {
        private readonly Lazy<string> _jsScript;
        private readonly Dictionary<string, Dictionary<string, string>> _dictionary;

        protected ByDictionaryBase(IDictionary<string, object> dictionary)
        {
            _jsScript = new Lazy<string>(() => InternalUtils.GetScript(GetScriptName()));
            _dictionary = new Dictionary<string, Dictionary<string, string>>();

            // We pass both the type and the value to the script that is executed in the browser,
            // so the browser script will know how to work with the received string (string or regular expression).
            _dictionary = dictionary.ToDictionary(pair => pair.Key, pair => new Dictionary<string, string>
            {
                {"type", pair.Value is Regex ? "RegExp" : "String"},
                {"value", pair.Value.ToString()}
            });
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var executor = InternalUtils.GetExecutor(context);
            var webElement = context as IWebElement;

            var executionResult = executor.ExecuteScript(
                _jsScript.Value,
                webElement,
                _dictionary);

            var elements = executionResult as ReadOnlyCollection<IWebElement> ?? new ReadOnlyCollection<IWebElement>(new List<IWebElement>());

            Description = GetDescription();

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException(GetDescription() + ": Cannot locate an element using " + ToString());
            }

            return elements[0];
        }

        protected abstract string GetScriptName();

        protected abstract string GetDescription();
    }
}