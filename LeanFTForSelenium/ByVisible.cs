using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace LeanFTForSelenium
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

            var elements = (ReadOnlyCollection<IWebElement>)executor.ExecuteScript(
                _jsScript,
                webElement,
                _visible);

            Description = string.Format("LeanFTForSelenium.By.Visible: {0}", _visible);

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }
    }
}