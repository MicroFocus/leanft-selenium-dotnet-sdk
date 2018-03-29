using System.Collections.Generic;

namespace LFT.Selenium
{
    internal class ByStyles : ByDictionaryBase
    {
        public ByStyles(IDictionary<string, object> styles) : base(styles)
        {
        }

        protected override string GetScriptName()
        {
            return "GetElementsByStyles.js";
        }

        protected override string GetDescription()
        {
            return "LFT.Selenium.By.Styles";
        }
    }
}