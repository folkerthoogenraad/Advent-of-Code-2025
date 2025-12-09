namespace AdventOfCode2025.Util;

public static class GraphExtensions
{
    extension<TVertex, TEdge>(Graph<TVertex, TEdge> graph)
    {
        public void AddBidirectionalEdge(TVertex from, TVertex to, TEdge data)
        {
            graph.AddEdge(from, to, data);
            graph.AddEdge(to, from, data);
        }
        
        public void RemoveBidirectionalEdge(TVertex from, TVertex to)
        {
            graph.RemoveEdge(from, to);
            graph.RemoveEdge(to, from);
        }
    }
}