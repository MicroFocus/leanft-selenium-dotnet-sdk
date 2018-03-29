using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing;
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
        const string PrepareForScreenshotScript = "var rect = arguments[0].getBoundingClientRect();return {left: rect.left,top:rect.top,width:rect.width,height:rect.height};";
        private static readonly Lazy<string> HighlightFunction = new Lazy<string>(() => InternalUtils.GetScript("Highlight.js"));
        private static readonly Lazy<string> ScrollIntoViewFunction = new Lazy<string>(() => InternalUtils.GetScript("ScrollIntoView.js"));
        private static readonly Lazy<string> SnapshotFunction = new Lazy<string>(() => InternalUtils.GetScript("Snapshot.js"));

        /// <summary>
        /// Returns a snapshot (image) of the selenium element.
        /// </summary>
        /// <param name="element">The element to retrieve a snapshot for.</param>
        /// <returns>A snapshot of the element.</returns>
        public static Image GetSnapshot(this IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Element cannot be null.");
            }

            var webDriver = InternalUtils.GetWebDriver(element);
            var executor = InternalUtils.GetExecutor(element);
            Dictionary<string, object> elementLocationAndSize;

            if (InternalUtils.IsVisible(element))
            {
                elementLocationAndSize = (Dictionary<string, object>) executor.ExecuteScript(PrepareForScreenshotScript, element);
            }
            else
            {
                elementLocationAndSize = (Dictionary<string, object>) executor.ExecuteScript(SnapshotFunction.Value, element);
            }

            var elementRectangle = new Rectangle(
                Convert.ToInt32(elementLocationAndSize["left"]),
                Convert.ToInt32(elementLocationAndSize["top"]),
                Convert.ToInt32(elementLocationAndSize["width"]),
                Convert.ToInt32(elementLocationAndSize["height"]));

            var screenshot = ((ITakesScreenshot) webDriver).GetScreenshot();
            var image = InternalUtils.Base64ToImage(screenshot.AsBase64EncodedString);
            var target = new Bitmap(elementRectangle.Width, elementRectangle.Height);

            using (var graphics = Graphics.FromImage(target))
            {
                graphics.DrawImage(image, new Rectangle(0, 0, elementRectangle.Width, elementRectangle.Height),
                    elementRectangle, GraphicsUnit.Pixel);
            }

            image.Dispose();

            return target;
        }

        /// <summary>
        /// Highlights the selenium element in the browser.
        /// </summary>
        /// <param name="element">The web element to highlight.</param>
        public static void Highlight(this IWebElement element)
        {
            Highlight(element, DefaultHighlightTimeInMiliSec);
        }

        /// <summary>
        /// Highlights the selenium elements in the browser for time milliseconds.
        /// </summary>
        /// <param name="element">The web element to highlight.</param>
        /// <param name="time">The time (in milliseconds) that the element will be highlighted. In case of a negative number, the method throws an ArgumentException.</param>
        public static void Highlight(this IWebElement element, int time)
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

            var executor = InternalUtils.GetExecutor(element);

            // Scroll into the view.
            ScrollIntoView(element);

            // Highlight the element.
            executor.ExecuteScript(HighlightFunction.Value, element, time);

            // Wait for the highlight to finish.
            Thread.Sleep(time + ExtraWaitTimeInMiliSec);
        }

        /// <summary>
        /// Scrolls the page to make the web element visible.
        /// </summary>
        /// <param name="element">The web element to scroll to.</param>
        public static void ScrollIntoView(this IWebElement element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("Cannot scroll into view null object.");
            }

            var executor = InternalUtils.GetExecutor(element);

            // Scroll into the view.
            if (!InternalUtils.IsVisible(element))
            {
                executor.ExecuteScript(ScrollIntoViewFunction.Value, element);
            }
        }
    }
}