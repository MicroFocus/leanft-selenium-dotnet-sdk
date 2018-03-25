using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    internal static class InternalUtils
    {
        internal static string GetBrowserScript(string scriptName)
        {
            string result;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LeanFTForSelenium.BrowserScripts." + scriptName;

            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        internal static IJavaScriptExecutor GetExecutor(ISearchContext context)
        {
            if (context is IJavaScriptExecutor)
            {
                return (IJavaScriptExecutor)context;
            }

            if (!(context is IWebElement))
            {
                throw new InvalidOperationException("Cannot find driver of this element.");
            }

            IWebDriver driver = ((IWrapsDriver)context).WrappedDriver;

            return (IJavaScriptExecutor)driver;
        }

        internal static bool IsVisible(IWebElement element)
        {
            var elementSize = element.Size;
            var elementLocation = element.Location;

            var driver = ((IWrapsDriver)element).WrappedDriver;
            var windowSize = driver.Manage().Window.Size;

            return !((elementSize.Width + elementLocation.X > windowSize.Width) || (elementSize.Height + elementLocation.Y > windowSize.Height));
        }

        internal static string FlagsToString(Regex pattern)
        {
            return pattern.Options.HasFlag(RegexOptions.IgnoreCase) ? "i" : "";
        }
    }
}