using System;
using System.Collections.Generic;
using System.Linq;
using BIO_Z7.Exceptions;
using QuickGraph;
using QuickGraph.Algorithms;

namespace BIO_Z7
{
    public class SubwordsGraph : AdjacencyGraph<string, TaggedEdge<string, string>>
    {
        public IEnumerable<Subword> SubwordsCollection { get; set; }

        public int k
        {
            get
            {
                var firstOrDefault = SubwordsCollection.FirstOrDefault();
                return firstOrDefault != null ? firstOrDefault.Length : 0;
            }
        }

        public LinkedList<string> GetEulerianTrail()
        {
            var currentVertex = this.OddVertices().Count == 0
                ? Vertices.FirstOrDefault()
                : GetUnevenStartVertex();
            var vertexStack = new Stack<string>();
            var eulerianVertices = new LinkedList<string>();
            var clone = Clone();

            while (vertexStack.Any() || clone.OutDegree(currentVertex) != 0)
            {
                if (clone.OutDegree(currentVertex) == 0)
                {
                    eulerianVertices.AddLast(currentVertex);
                    currentVertex = vertexStack.Pop();
                }
                else
                {
                    TaggedEdge<string, string> edgeToRemove;
                    var neighbourVertex = clone.GetAnyNeighbourOf(currentVertex);

                    if (clone.TryGetEdge(currentVertex, neighbourVertex, out edgeToRemove))
                        clone.RemoveEdge(edgeToRemove);
                    else
                        throw new Exception();

                    vertexStack.Push(currentVertex);
                    currentVertex = neighbourVertex;
                }
            }
            if (Edges.Count() != eulerianVertices.Count) throw new NoEulerianTrailException();
            return eulerianVertices;
        }

        private string GetUnevenStartVertex()
        {
            try
            {
                return Vertices.Single(v => OutDegree(v) - InDegree(v) > 0);
            }
            catch (InvalidOperationException e) { throw new NoEulerianTrailException(); }
        }

        private int InDegree(string vertex)
        {
            return Edges.Count(e => e.Target == vertex);
        }

        public string TrailToDnaSequence(LinkedList<string> vertexList)
        {
            var sequence = vertexList.Aggregate("",
                (current, vertex) => current + Vertices.Single(v => v == vertex).Last()).ToCharArray();
            Array.Reverse(sequence);
            return new string(sequence);
        }

        public string GetDnaSequence()
        {
            var eulerianTrail = GetEulerianTrail();
            return TrailToDnaSequence(eulerianTrail);
        }

        public CompensationResult GetDnaSequenceWithCompensation(int level)
        {
            //B��dy pozytywne - usuni�cie losowych s��w            
            foreach (var subword in SubwordsCollection.ToList().GetCombinations(level))
            {
                var shortenedSubwordsCollection = SubwordsCollection.ToList();
                shortenedSubwordsCollection.RemoveAll(e => subword.Contains(e));
                var shortenedGraph = SubwordsGraphAdapter.GetGraph(shortenedSubwordsCollection);
                try
                {
                    return new CompensationResult()
                    {
                        Graph = shortenedGraph,
                        DnaSequence = shortenedGraph.GetDnaSequence(),
                        CompensatingSubwords = null,
                        DeletedSubwords = string.Join(",",subword.Select(e => e.ToString()))
                    };
                }
                catch (NoEulerianTrailException) { }
            }
            //B��dy negatywne - dodanie losowych s��w
            foreach (var subword in SubwordFactory.GenerateAllPossible(k).GetCombinations(level))
            {
                var extenderSubwordsCollection = SubwordsCollection.ToList();
                extenderSubwordsCollection.AddRange(subword);
                var extendedGraph = SubwordsGraphAdapter.GetGraph(extenderSubwordsCollection);
                try
                {
                    return new CompensationResult()
                    {
                        Graph = extendedGraph,
                        DnaSequence = extendedGraph.GetDnaSequence(),
                        CompensatingSubwords = string.Join(",",subword.Select(e => e.ToString())),
                        DeletedSubwords = null
                    };
                }
                catch (NoEulerianTrailException) {}
            }
            //B��dy pozytywne i negatywne - usuni�cie i dodanie losowego s�owa
            foreach (var deletedSubwords in SubwordsCollection.ToList().GetCombinations(level))
            {
                foreach (var addedSubwords in SubwordFactory.GenerateAllPossible(k).GetCombinations(level))
                {
                    var alteredSubwordsCollection = SubwordsCollection.ToList();
                    alteredSubwordsCollection.RemoveAll(e => deletedSubwords.Contains(e));                    
                    alteredSubwordsCollection.AddRange(addedSubwords);
                    var extendedGraph = SubwordsGraphAdapter.GetGraph(alteredSubwordsCollection);
                    try
                    {
                        return new CompensationResult()
                        {
                            Graph = extendedGraph,
                            DnaSequence = extendedGraph.GetDnaSequence(),
                            CompensatingSubwords = string.Join(",",addedSubwords.Select(e => e.ToString())),
                            DeletedSubwords = string.Join(",", deletedSubwords.Select(e => e.ToString()))
                        };
                    }
                    catch (NoEulerianTrailException) {}
                }
            }
            throw new NoEulerianTrailException();
        }

        public struct CompensationResult
        {
            public string DnaSequence { get; set; }
            public string CompensatingSubwords { get; set; }
            public SubwordsGraph Graph { get; set; }
            public string DeletedSubwords { get; set; }
        }
    }
}