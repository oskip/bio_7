using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace BIO_Z7
{
    public static class Extensions
    {
        public static TVertex GetAnyNeighbourOf<TVertex, TEdge>(this AdjacencyGraph<TVertex, TEdge> graph, TVertex vertex) 
            where TEdge: IEdge<TVertex>
        {
            return graph.Edges.First(e => e.Source.Equals(vertex)).Target;
        }

        public static IEnumerable<IEnumerable<T>> GetCombinations<T>(this IEnumerable<T> values, int threshold)
        {
            var remaining = values;

            foreach (T value in values)
            {
                yield return value.Yield();

                if (threshold < 2)
                {
                    continue;
                }

                remaining = remaining.Skip(1);

                foreach (var combination in GetCombinations(remaining, threshold - 1))
                {
                    yield return value.Yield().Concat(combination);
                }
            }
        }

        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
    }
}