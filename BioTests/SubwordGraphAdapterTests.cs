using System.Linq;
using BIO_Z7;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BioTests
{
    [TestClass]
    public class SubwordGraphAdapterTests
    {
        private readonly SubwordGraphTests _subwordGraphTests = new SubwordGraphTests();

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
                if (!isEquivalent) 
                    Assert.Fail("Brak elementu "+connection.Source+"->"+connection.Target+" z tagiem "+connection.Tag+".");
            }
            Assert.IsTrue(isEquivalent);
        }

        [TestMethod]
        public void GraphVertexHasProperOutDegree()
        {
            var sut = SubwordsGraphAdapter.GetGraph(TestUtils.CorrectSubwords);
            Assert.AreEqual(3, sut.OutDegree("AC"));
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
    }
}
