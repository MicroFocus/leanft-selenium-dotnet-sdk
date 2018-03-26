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

        /// <summary>
        /// Returns a locator that locates elements by the provided role parameter.
        /// </summary>
        /// <param name="role">The role of the elements.</param>
        /// <returns>A locator that locates elements with the given role.</returns>
        public static By Role(string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new ArgumentNullException("Cannot find elements when role is null or empty.");
            }

            return new ByXPath("role", role);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression role parameter.
        /// </summary>
        /// <param name="role">The role of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given role.</returns>
        public static By Role(Regex role)
        {
            if (role == null)
            {
                throw new ArgumentNullException("Cannot find elements when role is null.");
            }

            return new ByRegex("role", role);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided type parameter.
        /// </summary>
        /// <param name="type">The type of the elements.</param>
        /// <returns>A locator that locates elements with the given type.</returns>
        public static By Type(string type)
        {
            if (string.IsNullOrEmpty(type))
            {
                throw new ArgumentNullException("Cannot find elements when type is null or empty.");
            }

            return new ByXPath("type", type);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression type parameter.
        /// </summary>
        /// <param name="type">The role of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the given type.</returns>
        public static By Type(Regex type)
        {
            if (type == null)
            {
                throw new ArgumentNullException("Cannot find elements when type is null.");
            }

            return new ByRegex("type", type);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided visibleText parameter.
        /// </summary>
        /// <param name="visibleText">The visible text of the elements.</param>
        /// <returns>A locator that locates elements by the visible text.</returns>
        public static By VisibleText(string visibleText)
        {
            if (string.IsNullOrEmpty(visibleText))
            {
                throw new ArgumentNullException("Cannot find elements when visibleText is null or empty.");
            }

            return new ByVisibleText(visibleText);
        }

        /// <summary>
        /// Returns a locator that locates element by the provided regular expression visibleText parameter.
        /// </summary>
        /// <param name="visibleText">The visible text of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements by the visible text.</returns>
        public static By VisibleText(Regex visibleText)
        {
            if (visibleText == null)
            {
                throw new ArgumentNullException("Cannot find elements when visibleText is null.");
            }

            return new ByVisibleText(visibleText);
        }

        /// <summary>
        /// Returns a locator that locates elements by the provided regular expression visibleText that is case insensitive.
        /// </summary>
        /// <param name="visibleText">The visible text of the elements in the form of a regular expression.</param>
        /// <param name="regexOptions">The regular expression options. For example, RegexOptions.IgnoreCase.</param>
        /// <returns>A locator that locates elements by the visible text with the given regular expression options.</returns>
        public static By VisibleText(Regex visibleText, RegexOptions regexOptions)
        {
            if (visibleText == null)
            {
                throw new ArgumentNullException("Cannot find elements when visibleText is null.");
            }

            return new ByVisibleText(new Regex(visibleText.ToString(), regexOptions));
        }
    }
}