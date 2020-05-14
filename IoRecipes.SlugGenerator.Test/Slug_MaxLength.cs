using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoRecipes.SlugGenerator.Test
{
    [TestClass]
    public class Slug_MaxLength
    {
        [TestMethod]
        public void MaxLength()
        {
            var slug = new Slug("abcdefgh", 2);

            Assert.AreEqual("ab", slug.GenerateSlug());

        }
    }
}
