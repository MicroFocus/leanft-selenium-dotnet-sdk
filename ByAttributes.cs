using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace LeanFTForSelenium
{
    internal class ByAttributes : By
    {
        private readonly string _jsScript = InternalUtils.GetScript("GetElementsByAttributes.js");
        private readonly IDictionary<string, IDictionary<string, string>> _attributes = new Dictionary<string, IDictionary<string, string>>();

        public ByAttributes(IDictionary<string, object> attributes)
        {
            // We pass both the type and the value to the script that is executed in the browser,
            // so the browser script will know how to work with the received string (string or regular expression).
            foreach (var attribute in attributes)
            {
                var attributeValue = new Dictionary<string, string>
                {
                    {"type", attribute.Value is Regex ? "RegExp" : "String"},
                    {"value", attribute.Value.ToString()}
                };

                _attributes.Add(attribute.Key, attributeValue);
            }
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var executor = InternalUtils.GetExecutor(context);
            var webElement = context as IWebElement;

            var elements = (ReadOnlyCollection<IWebElement>)executor.ExecuteScript(
                _jsScript,
                webElement,
                _attributes);

            Description = "LeanFTForSelenium.By.Attributes";

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }
    }
}