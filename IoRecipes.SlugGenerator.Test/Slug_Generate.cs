using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoRecipes.SlugGenerator.Test
{
    [TestClass]
    public class Slug_Generate
    {

        [TestMethod]
        public void SimpleInput_LowerCase()
        {
            var slug = new Slug("aBc");

            Assert.AreEqual("abc", slug.GenerateSlug());
            Assert.AreEqual("abc", slug.GenerateSlug(true));
        }

        [TestMethod]
        public void SimpleInput_Spaces()
        {
            var slug = new Slug("a c");

            Assert.AreEqual("a-c", slug.GenerateSlug());
            Assert.AreEqual("a-c", slug.GenerateSlug(true));
        }

        [TestMethod]
        public void SimpleInput_Dash()
        {
            var slug = new Slug("a-c");
            Assert.AreEqual("a-c", slug.GenerateSlug());
            Assert.AreEqual("a-c", slug.GenerateSlug(true));
        }


        [TestMethod]
        public void SimpleInput_DashAndSpaces()
        {
            var slug = new Slug("a - c");
            Assert.AreEqual("a-c", slug.GenerateSlug());
            Assert.AreEqual("a-c", slug.ToString());
            Assert.AreEqual("a-c", slug.GenerateSlug(true));
        }


        [DataTestMethod]
        [DataRow("jack&jones")]
        [DataRow("jack!@#jones")]
        [DataRow("jack$%^&*()+_jones")]
        [DataRow("jack&~_+**/<>,./;':[]{}`jones")]
        public void SimpleInput_SpecialChars(string input)
        {
            var slug = new Slug(input);
            Assert.AreEqual("jackjones", slug.GenerateSlug());
            Assert.AreEqual("jackjones", slug.GenerateSlug(true));
        }

    }
}
