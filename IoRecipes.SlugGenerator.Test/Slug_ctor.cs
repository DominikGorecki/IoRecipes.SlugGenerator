using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace IoRecipes.SlugGenerator.Test
{
    [TestClass]
    public class Slug_ctor
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NullParam()
        {
            var slug = new Slug(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void EmptyParam()
        {
            var slug = new Slug("");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void WhiteSpaceParam()
        {
            var slug = new Slug("  ");
        }


        [TestMethod]
        public void OnlySpecialChars()
        {
            var slug = new Slug("!@#%!");

            Assert.IsNull(slug.GenerateSlug());
            Assert.IsTrue(Guid.TryParse(slug.GenerateSlug(true), out _));
        }





    }
}
