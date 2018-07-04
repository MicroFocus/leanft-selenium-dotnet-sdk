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

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LeanFT.Selenium
{
    /// <summary>
    /// Contains all LeanFT for Selenium By methods for locating elements.
    /// </summary>
    public abstract class By : OpenQA.Selenium.By
    {
        /// <summary>
        /// Returns a locator that locates elements by the specified name, described using a regular expression.
        /// </summary>
        /// <param name="name">The name of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified name.</returns>
        public static By Name(Regex name)
        {
            if (name == null)
            {
                throw new ArgumentNullException("Cannot find elements when name is null.");
            }

            return new ByRegex("name", name);
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified ID, described using a regular expression.
        /// </summary>
        /// <param name="id">The ID of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified ID.</returns>
        public static By Id(Regex id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("Cannot find elements when id is null.");
            }

            return new ByRegex("id", id);

        }

        /// <summary>
        /// Returns a locator that locates elements by the specified className, described using a regular expression.
        /// </summary>
        /// <param name="className">The className of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified className.</returns>
        public static By ClassName(Regex className)
        {
            if (className == null)
            {
                throw new ArgumentNullException("Cannot find elements when className is null.");
            }

            return new ByRegex("className", className);
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified linkText, described using a regular expression.
        /// </summary>
        /// <param name="linkText">The linkText of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified linkText.</returns>
        public static By LinkText(Regex linkText)
        {
            if (linkText == null)
            {
                throw new ArgumentNullException("Cannot find elements when linkText is null.");
            }

            return new ByRegex("textContent", linkText, "a");
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified tagName, described using a regular expression.
        /// </summary>
        /// <param name="tagName">The tagName of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified tagName.</returns>
        public static By TagName(Regex tagName)
        {
            if (tagName == null)
            {
                throw new ArgumentNullException("Cannot find elements when tagName is null.");
            }

            return new ByRegex("tagName", new Regex(tagName.ToString(), tagName.Options | RegexOptions.IgnoreCase));
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified role.
        /// </summary>
        /// <param name="role">The role of the elements.</param>
        /// <returns>A locator that locates elements with the specified role.</returns>
        public static By Role(string role)
        {
            if (string.IsNullOrWhiteSpace(role))
            {
                throw new ArgumentNullException("Cannot find elements when role is null or empty.");
            }

            return new ByXPath("role", role);
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified role, described using a regular expression.
        /// </summary>
        /// <param name="role">The role of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified role.</returns>
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
        /// <returns>A locator that locates elements with the specified type.</returns>
        public static By Type(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
            {
                throw new ArgumentNullException("Cannot find elements when type is null or empty.");
            }

            return new ByXPath("type", type);
        }

        /// <summary>
        /// Returns a locator that locates elements by the specified type, described using a regular expression.
        /// </summary>
        /// <param name="type">The type of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements with the specified type.</returns>
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
        /// <returns>A locator that locates elements whose visible text matches the specified text.</returns>
        public static By VisibleText(string visibleText)
        {
            if (string.IsNullOrWhiteSpace(visibleText))
            {
                throw new ArgumentNullException("Cannot find elements when visibleText is null or empty.");
            }

            return new ByVisibleText(visibleText);
        }

        /// <summary>
        /// Returns a locator that locates element by the specified visibleText, described using a regular expression.
        /// </summary>
        /// <param name="visibleText">The visible text of the elements in the form of a regular expression.</param>
        /// <returns>A locator that locates elements whose visible text matches the specified text.</returns>
        public static By VisibleText(Regex visibleText)
        {
            if (visibleText == null)
            {
                throw new ArgumentNullException("Cannot find elements when visibleText is null.");
            }

            return new ByVisibleText(visibleText);
        }

        /// <summary>
        /// Returns a locator that locates elements that are either visible or not, depending on the parameter passed.
        /// </summary>
        /// <param name="visible">Whether the elements are visible.</param>
        /// <returns>A locator that locates elements that are either visible or not, depending on the parameter passed.</returns>
        public static By Visible(bool visible)
        {
            return new ByVisible(visible);
        }

        /// <summary>
        /// Returns a locator that locates elements according to one or more attributes. You can also use regular expressions.
        /// </summary>
        /// <param name="attributes">Dictionary of attributes.</param>
        /// <returns>A locator that locates elements that have the specified attribute values.</returns>
        public static By Attributes(IDictionary<string, object> attributes)
        {
            if (attributes == null)
            {
                throw new ArgumentNullException("Cannot find elements when attributes dictionary is null.");
            }

            if (attributes.Count == 0)
            {
                throw new ArgumentException("Cannot find elements when attributes dictionary is empty.");
            }

            return new ByAttributes(attributes);
        }

        /// <summary>
        /// Returns a locator that locates elements according to a single attribute.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The value of the attribute.</param>
        /// <returns>A locator that locates elements that have the specified attribute value.</returns>
        public static By Attribute(string name, string value)
        {
            return Attribute(name, (object) value);
        }

        /// <summary>
        /// Returns a locator that locates elements according to a single attribute, described using a regular expression.
        /// </summary>
        /// <param name="name">The name of the attribute.</param>
        /// <param name="value">The Pattern of the attribute.</param>
        /// <returns>A locator that locates elements whose attribute value matches the specified pattern.</returns>
        public static By Attribute(string name, Regex value)
        {
            return Attribute(name, (object) value);
        }

        private static By Attribute(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Cannot find elements when the name of the attribute is null or empty.");
            }

            return new ByAttributes(new Dictionary<string, object> {{name, value}});
        }

        /// <summary>
        /// Returns a locator that locates elements according to one or more styles. You can also use regular expressions.
        /// </summary>
        /// <param name="styles">Dictionary of styles.</param>
        /// <returns>A locator that locates elements with the specified styles.</returns>
        public static By Styles(IDictionary<string, object> styles)
        {
            if (styles == null)
            {
                throw new ArgumentNullException("Cannot find elements when styles dictionary is null.");
            }

            if (styles.Count == 0)
            {
                throw new ArgumentException("Cannot find elements when styles dictionary is empty.");
            }

            return new ByStyles(styles);
        }

        /// <summary>
        /// Returns a locator that locates elements according to a single style.
        /// </summary>
        /// <param name="name">The name of the style.</param>
        /// <param name="value">The value of the style.</param>
        /// <returns>A locator that locates elements with the specified style.</returns>
        public static By Style(string name, string value)
        {
            return Style(name, (object) value);
        }

        /// <summary>
        /// Returns a locator that locates elements according to a single style, described using a regular expression.
        /// </summary>
        /// <param name="name">The name of the style.</param>
        /// <param name="value">The Pattern of the style.</param>
        /// <returns>A locator that locates elements whose style matches the specified pattern.</returns>
        public static By Style(string name, Regex value)
        {
            return Style(name, (object) value);
        }

        private static By Style(string name, object value)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Cannot find elements when the name of the style is null or empty.");
            }

            return new ByStyles(new Dictionary<string, object> {{name, value}});
        }
    }
}