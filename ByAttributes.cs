using System.Collections.Generic;

namespace LFT.Selenium
{
    internal class ByAttributes : ByDictionaryBase
    {
        public ByAttributes(IDictionary<string, object> attributes) : base(attributes)
        {
        }

        protected override string GetScriptName()
        {
            return "GetElementsByAttributes.js";
        }

        protected override string GetDescription()
        {
            return "LeanFTForSelenium.By.Attributes";
        }
    }
}