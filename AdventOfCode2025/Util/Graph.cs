using System.Xml.Schema;

namespace AdventOfCode2025.Util;

public class Graph<TVertex, TEdge>
{
    public record Edge(TVertex To, TEdge Data);
    
    private List<TVertex> _vertices = new();
    private Dictionary<TVertex, List<Edge>> _edges = new();
    
    public void AddVertex(TVertex data)
    {
        _vertices.Add(data);
    }
    
    public void AddEdge(TVertex from, TVertex to, TEdge data)
    {
        var edges = GetOrCreateEdges(from);
        
        edges.Add(new Edge(to, data));
    }
    
    public bool HasEdge(TVertex from, TVertex to)
    {
        var edges = GetEdges(from);
        
        return edges.Any(x => EqualityComparer<TVertex>.Default.Equals(x.To, to));
    }
    
    public void RemoveEdge(TVertex from, TVertex to)
    {
        var edges = GetOrCreateEdges(from);

        edges.RemoveAll(x => EqualityComparer<TVertex>.Default.Equals(x.To, to));
    }
    
    public IReadOnlyList<Edge> GetEdges(TVertex vertex)
    {
        if(_edges.TryGetValue(vertex, out var edges))
        {
            return edges;
        }
        
        return [];
    }
    
    private List<Edge> GetOrCreateEdges(TVertex vertex)
    {
        if(_edges.TryGetValue(vertex, out var edges))
        {
            return edges;
        }
        
        edges = new();
        
        _edges[vertex] = edges;
        
        return edges;
    }
}