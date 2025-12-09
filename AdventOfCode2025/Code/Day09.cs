using System.Collections;
using System.Diagnostics;
using AdventOfCode2025.Util;

namespace AdventOfCode2025.Code;

public class Day09
{
    private record Coordinate(long X, long Y)
    {
        public static long AreaBetween(Coordinate a, Coordinate b)
        {
            long width = Math.Max(a.X, b.X) - Math.Min(a.X, b.X) + 1;
            long height = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y) + 1;
            
            return width * height;
        }
    }
    
    private record RemappedCoordinate(int X, int Y)
    {
        public int RX { get; set; }
        public int RY { get; set; }
        
        public Point Remapped => new Point(RX, RY);
        
        public static long AreaBetween(RemappedCoordinate a, RemappedCoordinate b)
        {
            long width = Math.Max(a.X, b.X) - Math.Min(a.X, b.X) + 1;
            long height = Math.Max(a.Y, b.Y) - Math.Min(a.Y, b.Y) + 1;
            
            return width * height;
        }
    }
    
    public long RunA(string input)
    {
        var coordinates = ParseCoordinates(input);
        
        long max = 0;
        
        foreach(var from in coordinates)
        {
            foreach(var to in coordinates)
            {
                var area = Coordinate.AreaBetween(from, to);
                
                if(area > max)
                {
                    max = area;
                }
            }
        }
        
        return max;
    }
    
    public long RunB(string input)
    {
        var coordinates = ParseCoordinates(input);
        
        var xCoordinates = coordinates.Select(x => x.X).Distinct().OrderBy(x => x).ToArray();
        var yCoordinates = coordinates.Select(x => x.Y).Distinct().OrderBy(x => x).ToArray();

        var remappedCoordinates = coordinates.Select(coordinate =>
        {
            var x = xCoordinates.IndexOf(coordinate.X);
            var y = yCoordinates.IndexOf(coordinate.Y);
            
            Debug.Assert(x >= 0);
            Debug.Assert(y >= 0);
            
            return new RemappedCoordinate((int)coordinate.X, (int)coordinate.Y) { RX = x, RY = y };
        }).ToArray();
        
        int width = remappedCoordinates.Max(x => x.RX) + 1;
        int height = remappedCoordinates.Max(x => x.RY) + 1;
        
        // Create the initial grid
        var grid = new Grid<char>(width, height);
        
        grid.Fill('.');
        
        foreach(var (from, to) in remappedCoordinates.Looped())
        {   
            foreach(var p in Point.Rectangle(from.Remapped, to.Remapped))
            {
                grid.Set(p, 'X');
            }
            
            grid.Set(from.Remapped, '#');
            grid.Set(to.Remapped, '#');
        }
        
        // Flood fill the grid
        var queue = new Queue<Point>();
        
        // Enqueue all the edges first. Doesn't really need to be _all_ done, but it is helpful since we don't know where the start is, and we need
        // to fill all of the outside.
        for(int i = 0; i < grid.Width; i++) queue.Enqueue(new Point(i, 0));
        for(int i = 0; i < grid.Width; i++) queue.Enqueue(new Point(i, grid.Height - 1));
        for(int i = 0; i < grid.Height; i++) queue.Enqueue(new Point(0, i));
        for(int i = 0; i < grid.Height; i++) queue.Enqueue(new Point(grid.Width - 1, i));
        
        while(queue.TryDequeue(out var point))
        {
            // We are out of bounds
            if(!grid.IsInBounds(point))
            {
                continue;
            }
            
            // We already filled this point
            if(grid.Get(point) == ' ')
            {
                continue;
            }
            
            // We are on a wall
            if(grid.Get(point) == '#' || grid.Get(point) == 'X')
            {
                continue;
            }
            
            // Write the outside space!
            grid.Set(point, ' ');
            
            queue.Enqueue(point.MoveX(-1));
            queue.Enqueue(point.MoveX(1));
            queue.Enqueue(point.MoveY(-1));
            queue.Enqueue(point.MoveY(1));
        }
        
        grid.Print();
        
        // Find the largest valid rectangle in this grid now.
        long max = 0;
        
        foreach(var from in remappedCoordinates)
        {
            foreach(var to in remappedCoordinates)
            {
                var area = RemappedCoordinate.AreaBetween(from, to);
             
                // If the area is smaller than the max, we won't even check the grid
                if(area <= max)
                {
                    continue;
                }
                
                if(Point.Rectangle(from.Remapped, to.Remapped).Any(point => grid.Get(point) == ' '))
                {
                    continue;
                }
                
                max = area;
            }
        }
        
        return max;
    }
    
    private Coordinate[] ParseCoordinates(string input)
    {
        var parts = input.Split('\n');
        
        return parts.Select(ParseCoordinate).ToArray();
    }
    
    private Coordinate ParseCoordinate(string input)
    {
        var parts = input.Split(',');
        
        return new Coordinate(
            int.Parse(parts[0]),
            int.Parse(parts[1]));
    }
}