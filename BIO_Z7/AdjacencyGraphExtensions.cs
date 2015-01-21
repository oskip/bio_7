using System.Linq;
using QuickGraph;

namespace BIO_Z7
{
    public static class AdjacencyGraphExtensions
    {
        public static TVertex GetAnyNeighbourOf<TVertex, TEdge>(this AdjacencyGraph<TVertex, TEdge> graph, TVertex vertex) 
            where TEdge: IEdge<TVertex>
        {
            return graph.Edges.First(e => e.Source.Equals(vertex)).Target;
        }
    }
}