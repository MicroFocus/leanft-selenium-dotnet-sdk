using System.Collections.ObjectModel;
using OpenQA.Selenium;

namespace LFT.Selenium
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