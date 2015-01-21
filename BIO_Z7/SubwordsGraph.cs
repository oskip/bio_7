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
            return eulerianVertices;
        }

        private string GetUnevenStartVertex()
        {
            try
            {
                return Vertices.Single(v => OutDegree(v) - InDegree(v) > 0);
            } catch(InvalidOperationException e) {throw new NoEulerianTrailException();}
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
    }
}