using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace IoRecipes.SlugGenerator.Test
{
    [TestClass]
    public class Slug_ExistingSlugs
    {

        [TestMethod]
        public void NullList()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(null);
            Assert.AreEqual("a-b", slug.GenerateSlug());
        }


        [TestMethod]
        public void EmptyList()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>());
            Assert.AreEqual("a-b", slug.GenerateSlug());
        }

        [TestMethod]
        public void NotMatching()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "a-c",
                "c-a"
            });
            Assert.AreEqual("a-b", slug.GenerateSlug());
        }

        [TestMethod]
        public void MatchingWrongCase()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "A-B"
            });
            Assert.AreEqual("a-b-1", slug.GenerateSlug());
        }


        [TestMethod]
        public void MatchingNoNumber()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "a-b-",
                "a-b"
            });
            Assert.AreEqual("a-b-1", slug.GenerateSlug());
        }


        [TestMethod]
        public void MatchingNumbered()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "a-b",
                "a-b-1",
            });
            Assert.AreEqual("a-b-2", slug.GenerateSlug());
        }

        [TestMethod]
        public void MatchingStringAfter()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "a-b",
                "a-b-d",
                "a-b-e",
                "a-b-1",
            });

            Assert.AreEqual("a-b-2", slug.GenerateSlug());
        }

        [TestMethod]
        public void MatchingHigherNumber()
        {
            var slug = new Slug("a b");
            slug.SetExistingSlugs(new List<string>
            { 
                "a-b",
                "a-b-1",
                "a-b-2",
                "a-b-13",
            });

            Assert.AreEqual("a-b-14", slug.GenerateSlug());
        }


    }
}
