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
            var subwordsCollection = subwords as IList<Subword> ?? subwords.ToList();
            foreach (var subword in subwordsCollection)
            {
                nodeSet.Add(subword.GetPrefix());
                nodeSet.Add(subword.GetPostfix());
            }

            var graph = new SubwordsGraph {SubwordsCollection = subwordsCollection};
            graph.AddVertexRange(nodeSet);
            graph = AddEgdesBetweenSubwords(graph, subwordsCollection, nodeSet);
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