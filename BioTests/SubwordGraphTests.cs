using System.Collections.Generic;
using BIO_Z7;
using BIO_Z7.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioTests
{
    [TestClass]
    public class SubwordGraphTests
    {
        [TestMethod]
        public void CanFindProperDnaSequence()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.SubwordsWithTrivialEulerianTrail);
            var trail = sut.GetEulerianTrail();
            var sequence = sut.TrailToDnaSequence(trail);
            Assert.AreEqual(TestUtils.TrivialSequence, sequence);
        }

        [TestMethod]
        public void CanFindEulerTrail()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            var trail = sut.GetEulerianTrail();
            var sequence = sut.TrailToDnaSequence(trail);
            Assert.IsNotNull(sequence);
        }

        [TestMethod]
        [ExpectedException(typeof (NoEulerianTrailException))]
        public void ThrowsProperExceptionWhenNoEulerianTrail()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.SubwordsWithoutEulerianTrail);
            var path = sut.GetEulerianTrail();
        }

        [TestMethod]
        public void EulerianTrailUsesAllEdges()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            var trail = sut.GetEulerianTrail();
            Assert.AreEqual(TestUtils.CorrectSubwordsConnections.Count, trail.Count);
        }
    }
}