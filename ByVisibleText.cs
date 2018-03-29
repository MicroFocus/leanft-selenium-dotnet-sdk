using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    internal class ByVisibleText : By
    {
        private readonly string _jsScript = InternalUtils.GetScript("GetElementsByVisibleText.js");
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
                _jsScript,
                _pattern.ToString(),
                InternalUtils.FlagsToString(_pattern),
                webElement,
                _nonRegex);

            Description = string.Format("LeanFTForSelenium.By.VisibleText: {0}", _pattern.ToString());

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }
    }
}