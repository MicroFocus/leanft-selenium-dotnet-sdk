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
    }
}