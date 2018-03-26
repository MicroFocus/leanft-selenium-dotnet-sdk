﻿using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    internal static class InternalUtils
    {
        internal static string GetScript(string scriptName)
        {
            string result;

            var assembly = Assembly.GetExecutingAssembly();
            var resourceName = "LeanFTForSelenium.BrowserScripts." + scriptName;

            using (var stream = assembly.GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            return result;
        }

        internal static IJavaScriptExecutor GetExecutor(ISearchContext context)
        {
            // If the context is the executor, return itself.
            var executor = context as IJavaScriptExecutor;
            if (executor != null)
            {
                return executor;
            }

            // If the context does not hold a WebDriver, we won't be able to return its driver.
            var wrapsDriver = context as IWrapsDriver;
            if (wrapsDriver == null)
            {
                throw new InvalidOperationException("Context does not have an IWebDriver.");
            }

            return (IJavaScriptExecutor) wrapsDriver.WrappedDriver;
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