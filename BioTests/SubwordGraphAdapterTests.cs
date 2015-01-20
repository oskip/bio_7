using System.Collections.Generic;
using System.Linq;
using BIO_Z7;
using BIO_Z7.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QuickGraph;

namespace BioTests
{
    [TestClass]
    public class SubwordGraphAdapterTests
    {
        [TestMethod]
        public void CanFormAGraphFromSubwords()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            Assert.IsNotNull(sut);
        }

        [TestMethod]
        public void GraphHasProperConnections()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            var isEquivalent = true;
            foreach (var connection in TestUtils.CorrectSubwordsConnections)
            {
                isEquivalent &= sut.Edges.Any(
                    e => ((e.Source == connection.Source) && (e.Target == connection.Target) && (e.Tag == connection.Tag)));
                if (!isEquivalent) Assert.Fail("Brak elementu "+connection.Source+"->"+connection.Target+" z tagiem "+connection.Tag+".");
            }
            Assert.IsTrue(isEquivalent);
        }

        [TestMethod]
        public void GraphVertexHasProperDegree()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            Assert.AreEqual(sut.OutDegree("GA"), 2);
        }

        [TestMethod]
        public void GraphHasProperEdgesNumber()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            Assert.AreEqual(TestUtils.CorrectSubwordsConnections.Count, sut.Edges.Count());
        }

        [TestMethod]
        public void GraphHasProperVerticesNumber()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            Assert.AreEqual(TestUtils.CorrectSubwordsVerticesCount, sut.Vertices.Count());
        }

        [TestMethod]
        public void GraphDoesntHaveTwoSameVertices()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            CollectionAssert.AllItemsAreUnique(sut.Vertices.ToList());
        }

        [TestMethod]
        public void CanFindEulerianPath()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            var path = sut.GetEulerianPath();
            CollectionAssert.AreEqual(new LinkedList<string>(new List<string>()
            {
                "AC","CT","TG","GA","AC","CA","AC","CA"
            }), path);
        }

        [TestMethod]
        [ExpectedException(typeof (NoEulerianPathException))]
        public void ThrowsProperExceptionWhenNoEulerianPath()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            var path = sut.GetEulerianPath();
        }
    }
}
