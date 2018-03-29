using OpenQA.Selenium;
using OpenQA.Selenium.Internal;

namespace LeanFTForSelenium
{
    internal class ByXPath : By
    {
        private readonly string _attributeName;
        private readonly string _xPath;

        public ByXPath(string attributeName, string xPath)
        {
            _attributeName = attributeName;
            _xPath = xPath;
        }

        public override System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementsByXPath(string.Format(".//*[@{0} = '{1}']", _attributeName, _xPath));
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return ((IFindsByXPath) context).FindElementByXPath(string.Format(".//*[@{0} = '{1}']", _attributeName, _xPath));
        }
    }
}