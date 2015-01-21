using System.Collections.Generic;
using System.Linq;
using QuickGraph;

namespace BIO_Z7
{
    public class SubwordsGraphAdapter
    {
        public static SubwordsGraph GetGraph(IEnumerable<Subword> subwords)
        {
            var nodeSet = new HashSet<string>();
            foreach (var subword in subwords)
            {
                nodeSet.Add(subword.GetPrefix());
                nodeSet.Add(subword.GetPostfix());
            }

            var graph = new SubwordsGraph();
            graph.AddVertexRange(nodeSet);
            graph = AddEgdesBetweenSubwords(graph, subwords, nodeSet);
            return graph;
        }

        private static SubwordsGraph AddEgdesBetweenSubwords(SubwordsGraph graph, IEnumerable<Subword> subwords, HashSet<string> nodeNameSet)
        {
            foreach (var subword in subwords)
            {
                var subwordEndLetter = subword.ToString().Last().ToString();
                var edge = new TaggedEdge<string, string>(subword.GetPrefix(), subword.GetPostfix(), subwordEndLetter);
                if (!graph.ContainsEdge(edge))
                {
                    graph.AddEdge(edge);
                }
            }
            return graph;
        }
    } 
}