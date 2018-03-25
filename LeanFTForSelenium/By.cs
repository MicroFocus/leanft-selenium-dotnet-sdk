using System;
using System.Text.RegularExpressions;

namespace LeanFTForSelenium
{
    /// <summary>
    /// Contains all LeanFT for Selenium By methods for locating elements.
    /// </summary>
    public abstract class By : OpenQA.Selenium.By
    {
        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression name parameter.
        /// </summary>
        /// <param name="name">The name of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given name.</returns>
        public static By Name(Regex name)
        {
		    if (name == null) {
                throw new ArgumentNullException("Cannot find elements when name is null.");
		    }

            return new ByRegex("name", name);
	    }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression ID parameter.
        /// </summary>
        /// <param name="id">The ID of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given ID.</returns>
        public static By Id(Regex id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Cannot find elements when id is null.");
            }

            return new ByRegex("id", id);

        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression className parameter.
        /// </summary>
        /// <param name="className">The className of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given className.</returns>
        public static By ClassName(Regex className)
        {
            if (className == null)
            {
                throw new ArgumentNullException("Cannot find elements when className is null.");
            }

            return new ByRegex("className", className);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression linkText parameter.
        /// </summary>
        /// <param name="linkText">The linkText of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given linkText.</returns>
        public static By LinkText(Regex linkText)
        {
            if (linkText == null)
            {
                throw new ArgumentNullException("Cannot find elements when linkText is null.");
            }

            return new ByRegex("textContent", linkText, "a");
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression tagName parameter.
        /// </summary>
        /// <param name="tagName">The tagName of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given tagName.</returns>
        public static By TagName(Regex tagName)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException("Cannot find elements when tagName is null.");
            }

            return new ByRegex("tagName", tagName);
        }
    }
}