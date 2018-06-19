/*! (c) Copyright 2015 - 2018 Micro Focus or one of its affiliates. */
//
// Licensed under the Apache License, Version 2.0 (the "License");
// You may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0
//
// Apache License 2.0 - Apache Software Foundation
// www.apache.org
// Apache License Version 2.0, January 2004 http://www.apache.org/licenses/ TERMS AND CONDITIONS FOR USE, REPRODUCTION ...
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace LeanFT.Selenium
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

        internal static IWebDriver GetWebDriver(IWebElement element)
        {
            var wrapsDriver = element as IWrapsDriver;
            if (wrapsDriver == null)
            {
                return null;
            }

            return wrapsDriver.WrappedDriver;
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

            var driver = ((IWrapsDriver) element).WrappedDriver;
            var windowSize = driver.Manage().Window.Size;

            return !((elementSize.Width + elementLocation.X > windowSize.Width) ||
                     (elementSize.Height + elementLocation.Y > windowSize.Height));
        }

        internal static string FlagsToString(Regex pattern)
        {
            return pattern.Options.HasFlag(RegexOptions.IgnoreCase) ? "i" : "";
        }

        internal static Image Base64ToImage(string base64String)
        {
            var imageBytes = Convert.FromBase64String(base64String);
            var memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length);
            memoryStream.Write(imageBytes, 0, imageBytes.Length);
            var image = Image.FromStream(memoryStream, true);

            return image;
        }
    }
}