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
using NUnit.Framework;

namespace LeanFT.Selenium.UnitTests
{
    [TestFixture]
    class ByUnitTests
    {
        [Test]
        public void Name_NameIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Name(null));
        }

        [Test]
        public void Name_ShouldReturnByRegexObject()
        {
            var by = By.Name(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void Id_IdIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Id(null));
        }

        [Test]
        public void Id_ShouldReturnByRegexObject()
        {
            var by = By.Id(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void ClassName_ClassNameIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.ClassName(null));
        }

        [Test]
        public void ClassName_ShouldReturnByRegexObject()
        {
            var by = By.ClassName(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void LinkText_LinkTextIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.LinkText(null));
        }

        [Test]
        public void LinkText_ShouldReturnByRegexObject()
        {
            var by = By.LinkText(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void TagName_TagNameIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.TagName(null));
        }

        [Test]
        public void TagName_ShouldReturnByRegexObject()
        {
            var by = By.TagName(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void Role_String_RoleIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Role((string) null));
        }

        [Test]
        public void Role_String_ShouldReturnByXPathObject()
        {
            var by = By.Role("role");
            Assert.IsInstanceOf<ByXPath>(by);
        }

        [Test]
        public void Role_Regex_RoleIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Role((Regex) null));
        }

        [Test]
        public void Role_Regex_ShouldReturnByRegexObject()
        {
            var by = By.Role(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void Type_String_TypeIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Type((string) null));
        }

        [Test]
        public void Type_String_ShouldReturnByXPathObject()
        {
            var by = By.Type("type");
            Assert.IsInstanceOf<ByXPath>(by);
        }

        [Test]
        public void Type_Regex_TypeIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Type((Regex) null));
        }

        [Test]
        public void Type_Regex_ShouldReturnByRegexObject()
        {
            var by = By.Type(new Regex(""));
            Assert.IsInstanceOf<ByRegex>(by);
        }

        [Test]
        public void VisibleText_String_VisibleTextIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.VisibleText((string) null));
        }

        [Test]
        public void VisibleText_String_ShouldReturnByVisibleTextObject()
        {
            var by = By.VisibleText("text");
            Assert.IsInstanceOf<ByVisibleText>(by);
        }

        [Test]
        public void VisibleText_Regex_VisibleTextIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.VisibleText((Regex) null));
        }

        [Test]
        public void VisibleText_Regex_ShouldReturnByVisibleTextObject()
        {
            var by = By.VisibleText(new Regex("text"));
            Assert.IsInstanceOf<ByVisibleText>(by);
        }

        [Test]
        public void Visible_ShouldReturnByVisibleObject()
        {
            var by = By.Visible(true);
            Assert.IsInstanceOf<ByVisible>(by);
        }

        [Test]
        public void Attributes_AttributesIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Attributes(null));
        }

        [Test]
        public void Attributes_AttributesIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Attributes(new Dictionary<string, object>()));
        }

        [Test]
        public void Attributes_ShouldReturnByAttributesObject()
        {
            var by = By.Attributes(new Dictionary<string, object> {{"a", "1"}});
            Assert.IsInstanceOf<ByAttributes>(by);
        }

        [Test]
        public void Attribute_StringValue_NameIsNull_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Attribute(null, ""));
        }

        [Test]
        public void Attribute_StringValue_NameIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Attribute("", ""));
        }

        [Test]
        public void Attribute_StringValue_ShouldReturnByAttributesObject()
        {
            var by = By.Attribute("a", "1");
            Assert.IsInstanceOf<ByAttributes>(by);
        }

        [Test]
        public void Attribute_RegexValue_NameIsNull_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Attribute(null, new Regex("")));
        }

        [Test]
        public void Attribute_RegexValue_NameIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Attribute("", new Regex("")));
        }

        [Test]
        public void Attribute_RegexValue_ShouldReturnByAttributesObject()
        {
            var by = By.Attribute("a", new Regex(""));
            Assert.IsInstanceOf<ByAttributes>(by);
        }

        [Test]
        public void Styles_StylesIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.Styles(null));
        }

        [Test]
        public void Styles_StylesIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Styles(new Dictionary<string, object>()));
        }

        [Test]
        public void Styles_ShouldReturnByStylesObject()
        {
            var by = By.Styles(new Dictionary<string, object> { { "a", "1" } });
            Assert.IsInstanceOf<ByStyles>(by);
        }

        [Test]
        public void Style_StringValue_NameIsNull_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Style(null, ""));
        }

        [Test]
        public void Style_StringValue_NameIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Style("", ""));
        }

        [Test]
        public void Style_StringValue_ShouldReturnByStylesObject()
        {
            var by = By.Style("a", "1");
            Assert.IsInstanceOf<ByStyles>(by);
        }

        [Test]
        public void Style_RegexValue_NameIsNull_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Style(null, new Regex("")));
        }

        [Test]
        public void Style_RegexValue_NameIsEmpty_ArgumentExceptionIsThrown()
        {
            Assert.Throws<ArgumentException>(() => By.Style("", new Regex("")));
        }

        [Test]
        public void Style_RegexValue_ShouldReturnByStylesObject()
        {
            var by = By.Style("a", new Regex(""));
            Assert.IsInstanceOf<ByStyles>(by);
        }
    }
}