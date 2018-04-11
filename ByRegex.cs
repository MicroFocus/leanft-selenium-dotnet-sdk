using System;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LFT.Selenium
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
            var elements = (ReadOnlyCollection<IWebElement>) executor.ExecuteScript(
                _jsScript.Value,
                element,
                _tagsFilter,
                _propertyName,
                _pattern.ToString(),
                InternalUtils.FlagsToString(_pattern));

            return elements;
        }
    }
}