using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    internal class ByRegex : By
    {
        private readonly string _jsScript = InternalUtils.GetScript("GetElementsByRegExp.js");
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
            if (context is IWebElement)
            {
                // In case element is the context, pass it to the method as the root
                // element to search in.
                elements = FindElementsByRegex(executor, (IWebElement) context);
            }
            else
            {
                // In case the driver is the root element
                elements = FindElementsByRegex(executor, null);
            }

            Description = string.Format("LeanFTForSelenium.By.{0}: {1}", _propertyName, _pattern.ToString());

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }

        private ReadOnlyCollection<IWebElement> FindElementsByRegex(IJavaScriptExecutor executor, IWebElement element)
        {
            var elements = (ReadOnlyCollection<IWebElement>) executor.ExecuteScript(
                _jsScript,
                element,
                _tagsFilter,
                _propertyName,
                _pattern.ToString(),
                InternalUtils.FlagsToString(_pattern));

            return elements;
        }
    }
}