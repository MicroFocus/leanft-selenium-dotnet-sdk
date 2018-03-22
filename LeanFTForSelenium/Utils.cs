using OpenQA.Selenium;
using System;
using System.Threading;

namespace LeanFTForSelenium
{
    /// <summary>
    /// LeanFT for Selenium utilities.
    /// </summary>
    public static class Utils
    {
        const int ExtraWaitTimeInMiliSec = 500;
        const int DefaultHighlightTimeInMiliSec = 3000;
        static string HighlightFunction = InternalUtils.GetBrowserScript("Highlight.js");
        static string ScrollIntoViewFunction = InternalUtils.GetBrowserScript("ScrollIntoView.js");

        /// <summary>
        /// Highlights the selenium element in the browser.
        /// </summary>
        /// <param name="element">The web element to highlight.</param>

        public static void Highlight(IWebElement element)
        {
            Highlight(element, DefaultHighlightTimeInMiliSec);
        }

        /// <summary>
        /// Highlights the selenium elements in the browser for time milliseconds.
        /// </summary>
        /// <param name="element">The web element to highlight.</param>
        /// <param name="time">The time (in milliseconds) that the element will be highlighted. In case of a negative number, the method throws an ArgumentException.</param>
        public static void Highlight(IWebElement element, int time)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Cannot highlight null object.");
            }

            if (time < 0)
            {
                throw new ArgumentException("The time parameter can't be negative.");
            }

            if (time == 0)
            {
                return;
            }

            IJavaScriptExecutor executor = InternalUtils.GetExecutor(element);

            // Scroll into the view.
            ScrollIntoView(element);

            // Highlight the element.
            executor.ExecuteScript(HighlightFunction, element, time);

            // Wait for the highlight to finish.
            Thread.Sleep(time + ExtraWaitTimeInMiliSec);
        }

        /// <summary>
        /// Scrolls the page to make the web element visible.
        /// </summary>
        /// <param name="element">The web element to scroll to.</param>
        public static void ScrollIntoView(IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Cannot scroll into view null object.");
            }

            IJavaScriptExecutor executor = InternalUtils.GetExecutor(element);

            // Scroll into the view.
            if (!InternalUtils.IsVisible(element))
            {
                executor.ExecuteScript(ScrollIntoViewFunction, element);
            }
        }
    }
}