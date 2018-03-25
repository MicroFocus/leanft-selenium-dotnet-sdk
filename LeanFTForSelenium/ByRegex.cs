using OpenQA.Selenium;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    internal class ByRegex : By
    {
        private readonly string jsScript = InternalUtils.GetBrowserScript("GetElementsByRegExp.js");
        private string propertyName;
        private string tagsFilter;
        private Regex pattern;

        internal ByRegex(string propertyName, Regex pattern, string tagsFilter)
        {
            this.propertyName = propertyName;
            this.pattern = pattern;
            this.tagsFilter = tagsFilter;
        }

        internal ByRegex(string propertyName, Regex pattern) : this(propertyName, pattern, null) { }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            ReadOnlyCollection<IWebElement> elements;

            IJavaScriptExecutor executor = InternalUtils.GetExecutor(context);

            if (context is IWebElement){
                // In case element is the context, pass it to the method as the root
                // element to search in.
                elements = FindElementsByRegex(executor, (IWebElement)context);
            } else {
                // In case the driver is the root element
                elements = FindElementsByRegex(executor, null);
            }

            Description = string.Format("LeanFTForSelenium.By.{0}: {1}", propertyName, pattern.ToString());

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }

        private ReadOnlyCollection<IWebElement> FindElementsByRegex(IJavaScriptExecutor executor, IWebElement element)
        {
            ReadOnlyCollection<IWebElement> elements =
                (ReadOnlyCollection<IWebElement>)executor.ExecuteScript(
                    jsScript,
                    element,
                    tagsFilter,
                    propertyName,
                    pattern.ToString(),
                    InternalUtils.FlagsToString(pattern));

            return elements;
        }
    }
}