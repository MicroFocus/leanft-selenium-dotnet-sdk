using System;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace LeanFTForSelenium.UnitTests
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
        public void VisibleText_RegexWithOptions_VisibleTextIsNull_ArgumentNullExceptionIsThrown()
        {
            Assert.Throws<ArgumentNullException>(() => By.VisibleText(null, RegexOptions.IgnoreCase));
        }

        [Test]
        public void VisibleText_RegexWithOptions_ShouldReturnByVisibleTextObject()
        {
            var by = By.VisibleText(new Regex("text"), RegexOptions.IgnoreCase);
            Assert.IsInstanceOf<ByVisibleText>(by);
        }

        [Test]
        public void Visible_ShouldReturnByVisibleObject()
        {
            var by = By.Visible(true);
            Assert.IsInstanceOf<ByVisible>(by);
        }
    }
}