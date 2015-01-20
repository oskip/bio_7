using System.Collections.Generic;
using System.Linq;
using BIO_Z7;
using BIO_Z7.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioTests
{
    [TestClass]
    public class SubwordTests
    {
        [TestMethod]
        public void CanTakePrefixFromSubword()
        {
            var sut = new Subword(TestUtils.CorrectSubword);
            var prefix = sut.GetPrefix();
            Assert.AreEqual(TestUtils.CorrectSubwordsPrefix, prefix.ToString());
        }

        [TestMethod]
        public void CanTakePostfixFromSubword()
        {
            var sut = new Subword(TestUtils.CorrectSubword);
            var postfix = sut.GetPostfix();
            Assert.AreEqual(TestUtils.CorrectSubwordsPostfix, postfix.ToString());
        }

        [TestMethod]
        public void CanFormASubwordOfLength5()
        {
            var sut = new Subword(TestUtils.CorrectSubwordFiveString);
            Assert.AreEqual(TestUtils.CorrectSubwordFiveString, sut.ToString());
        }

        [TestMethod]
        [ExpectedException(typeof(BadSymbolException))]
        public void ThrowsExceptionWhenBadSymbolInConstructor()
        {
            var sut = new Subword(TestUtils.IncorrectSubword);
        }

        [TestMethod]
        public void CanGenerateCorrectNumberOfCombinations()
        {
            var sut = SubwordFactory.GenerateAllPossible(8);
            Assert.AreEqual(65536, sut.Count());
        }

        [TestMethod]
        public void CanGenerateSubwordsFromInputString()
        {
            var sut = SubwordFactory.CreateFromInput(TestUtils.CorrectInputString);
            CollectionAssert.AllItemsAreNotNull(sut.ToList());
        }

        [TestMethod]
        [ExpectedException(typeof(InconsistentSubwordLengthException))]
        public void ThrowsExceptionWhenOneOfInputSubwordsIsTooLong()
        {
            var sut = SubwordFactory.CreateFromInput(TestUtils.InputStringWithOneTooLong);
        }

        [TestMethod]
        [ExpectedException(typeof (InconsistentSubwordLengthException))]
        public void ThrowsExceptionWhenOneOfInputSubwordsIsTooShort()
        {
            var sut = SubwordFactory.CreateFromInput(TestUtils.InputStringWithOneTooShort);
            Assert.Equals(sut, "a");
        }
    }
}
