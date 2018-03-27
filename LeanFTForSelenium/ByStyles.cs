using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace LeanFTForSelenium
{
    internal class ByStyles : By
    {
        private readonly string _jsScript = InternalUtils.GetScript("GetElementsByStyles.js");
        private readonly IDictionary<string, IDictionary<string, string>> _styles = new Dictionary<string, IDictionary<string, string>>();

        public ByStyles(IDictionary<string, object> styles)
        {
            // We pass both the type and the value to the script that is executed in the browser,
            // so the browser script will know how to work with the received string (string or regular expression).
            foreach (var style in styles)
            {
                var styleValue = new Dictionary<string, string>
                {
                    {"type", style.Value is Regex ? "RegExp" : "String"},
                    {"value", style.Value.ToString()}
                };

                _styles.Add(style.Key, styleValue);
            }
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var executor = InternalUtils.GetExecutor(context);
            var webElement = context as IWebElement;

            var elements = (ReadOnlyCollection<IWebElement>)executor.ExecuteScript(
                _jsScript,
                webElement,
                _styles);

            Description = "LeanFTForSelenium.By.Styles";

            return elements;
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            return FindElements(context)[0];
        }
    }
}