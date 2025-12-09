using System.Collections;
using System.Diagnostics;
using AdventOfCode2025.Util;

namespace AdventOfCode2025.Code;

public class Day08
{
    public record Coordinate(int X, int Y, int Z)
    {
        public static double Distance(Coordinate a, Coordinate b)
        {
            double dx = a.X - b.X;
            double dy = a.Y - b.Y;
            double dz = a.Z - b.Z;
            
            return Math.Sqrt(dx * dx + dy * dy + dz * dz);
        }
    }
    
    public long RunA(string input)
    {
        var coordinates = ParseCoordinates(input);
        var matrix = GetDistanceMatrix(coordinates);
        var allEdges = matrix.AllPoints
            .Select(x => (From: coordinates[x.X], To: coordinates[x.Y], Distance: matrix.Get(x)))
            .OrderBy(x => x.Distance);

        var graph = new Graph<Coordinate, int>();
        var colors = new Dictionary<Coordinate, int>();

        // Add the 1000 connections
        var connections = 0;
        
        foreach (var edge in allEdges)
        {
            // Dont connect to yourself
            if(edge.From == edge.To) continue;
            
            // Dont do the reverse connection either
            if(graph.HasEdge(edge.From, edge.To)) continue; 
            
            connections += 1;
            
            graph.AddBidirectionalEdge(edge.From, edge.To, 1);
            
            if(connections >= 1000) // Example is with 10, otherwise 1000
            {
                break;
            }
        }
        
        // Color the graph
        int currentColor = 1;
        
        foreach(var coordinate in coordinates)
        {
            if(colors.ContainsKey(coordinate))
            {
                continue;
            }
            
            var visited = new HashSet<Coordinate>();
            var queue = new Queue<Coordinate>();
            
            queue.Enqueue(coordinate);
            
            while(queue.TryDequeue(out var current))
            {
                if(visited.Contains(current)) continue;
                
                visited.Add(current);
                colors.Add(current, currentColor);
                
                foreach(var edge in graph.GetEdges(current))
                {
                    queue.Enqueue(edge.To);
                }
            }
            
            Debug.Assert(colors.ContainsKey(coordinate));
            
            currentColor += 1;
        }
        
        // Get the groups we just created
        var groups = coordinates
            .GroupBy(x => colors[x])
            .Select(x => (Group: x.Key, GroupSize: x.Count()))
            .OrderByDescending(x => x.GroupSize);
        
        return groups.Take(3).Aggregate(1, (a, b) => a * b.GroupSize);
    }
    
    public long RunB(string input)
    {
        var coordinates = ParseCoordinates(input);
        var matrix = GetDistanceMatrix(coordinates);
        var allEdges = matrix.AllPoints
            .Select(x => (From: coordinates[x.X], To: coordinates[x.Y], Distance: matrix.Get(x)))
            .OrderBy(x => x.Distance);
        
        var graph = new Graph<Coordinate, int>();
        var toVisit = coordinates.ToHashSet();

        foreach (var edge in allEdges)
        {
            // Dont connect to yourself
            if(edge.From == edge.To) continue;
            
            // Dont do the reverse connection either
            if(graph.HasEdge(edge.From, edge.To)) continue; 
            
            graph.AddBidirectionalEdge(edge.From, edge.To, 1);
            
            toVisit.Remove(edge.From);
            toVisit.Remove(edge.To);
            
            if(toVisit.Count == 0)
            {
                return ((long)edge.From.X) * ((long)edge.To.X);
            }
        }
        
        throw new Exception("Never found a full connection, which is totally impossible");
    }
    
    public Grid<double> GetDistanceMatrix(Coordinate[] coordinates)
    {
        var grid = new Grid<double>(coordinates.Length, coordinates.Length);
        
        grid.Foreach((point, value) =>
        {
            grid.Set(point, Coordinate.Distance(coordinates[point.X], coordinates[point.Y]));
        });
        
        return grid;
    }
    
    public Coordinate[] ParseCoordinates(string input)
    {
        var parts = input.Split('\n');
        
        return parts.Select(ParseCoordinate).ToArray();
    }
    
    public Coordinate ParseCoordinate(string input)
    {
        var parts = input.Split(',');
        
        return new Coordinate(
            int.Parse(parts[0]),
            int.Parse(parts[1]),
            int.Parse(parts[2]));
    }
}