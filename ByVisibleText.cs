using System;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LFT.Selenium
{
    internal class ByVisibleText : By
    {
        private readonly Lazy<string> _jsScript = new Lazy<string>(() => InternalUtils.GetScript("GetElementsByVisibleText.js"));
        private readonly Regex _pattern;
        private readonly bool _nonRegex;

        internal ByVisibleText(string visibleText) : this(new Regex(visibleText))
        {
            _nonRegex = true;
        }

        internal ByVisibleText(Regex pattern)
        {
            _pattern = pattern;
            _nonRegex = false;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var executor = InternalUtils.GetExecutor(context);
            var webElement = context as IWebElement;

            var elements = (ReadOnlyCollection<IWebElement>) executor.ExecuteScript(
                _jsScript.Value,
                _pattern.ToString(),
                InternalUtils.FlagsToString(_pattern),
                webElement,
                _nonRegex);

            Description = string.Format("LFT.Selenium.By.VisibleText: {0}", _pattern);

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("By.VisibleText: Cannot locate an element using " + ToString());
            }

            return elements[0];
        }
    }
}