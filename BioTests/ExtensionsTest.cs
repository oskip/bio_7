using System;
using System.Collections.Generic;
using System.Linq;
using BIO_Z7;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioTests
{
    [TestClass]
    public class ExtensionsTest
    {
        [TestMethod]
        public void CanGenerateProperNumberOfCombinationsOfEnumerable()
        {
            var collection = new List<int>() {1, 2, 3};
            var sut = collection.GetCombinations(2);
            Assert.AreEqual(6, sut.Count());
        }
    }
}
