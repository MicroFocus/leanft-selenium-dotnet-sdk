using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using OpenQA.Selenium;

namespace LeanFTForSelenium
{
    public class ByAny : By
    {
        private readonly OpenQA.Selenium.By[] _bys;

        public ByAny(params OpenQA.Selenium.By[] bys)
        {
            _bys = bys;
        }

        public override ReadOnlyCollection<IWebElement> FindElements(ISearchContext context)
        {
            var elements = new List<IWebElement>();

            foreach (var by in _bys)
            {
                elements.AddRange(by.FindElements(context));
            }

            return new ReadOnlyCollection<IWebElement>(elements);
        }

        public override IWebElement FindElement(ISearchContext context)
        {
            var elements = FindElements(context);

            if (elements.Count == 0)
            {
                throw new NoSuchElementException("ByAny: Cannot locate an element using " + ToString());
            }

            return elements[0];
        }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder("ByAny(");

            for (var i = 0; i < _bys.Length; i++)
            {
                stringBuilder.Append(_bys[i]);
                if (i != _bys.Length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            stringBuilder.Append(")");

            return stringBuilder.ToString();
        }
    }
}