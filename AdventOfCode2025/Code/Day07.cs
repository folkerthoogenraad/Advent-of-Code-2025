using System.Diagnostics;
using AdventOfCode2025.Util;

namespace AdventOfCode2025.Code;

public class Day07
{
    public long RunA(string str)
    {
        var grid = Grid<char>.Parse(str, c => c);
        
        grid.Print();
        
        var start = grid.Find(x => x == 'S');
        
        Debug.Assert(start.HasValue);
        
        long count = 0;
        
        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        
        queue.Enqueue(start.Value);
        
        while(queue.TryDequeue(out var current))
        {
            if(visited.Contains(current)) continue;
            
            visited.Add(current);
            
            grid.Set(current, '|');
            
            if(current.Y == grid.Height - 1)
            {
                continue;
            }
            
            var below = current.MoveY(1);
            
            // Split
            if(grid.Get(below) == '^')
            {
                count += 1;

                queue.Enqueue(below.MoveX(-1));
                queue.Enqueue(below.MoveX(1));
            }
            // Normal
            else
            {
                queue.Enqueue(below);
            }
        }
        
        grid.Print();
        
        return count;
    }
    
    public long RunB(string str)
    {
        var grid = Grid<char>.Parse(str, c => c);
        
        var start = grid.Find(x => x == 'S');
        var pathCountGrid = grid.Map((_, _) => 0L);
        
        Debug.Assert(start.HasValue);
        
        pathCountGrid.Set(start.Value, 1);
        
        var queue = new Queue<Point>();
        var visited = new HashSet<Point>();
        
        queue.Enqueue(start.Value);
        
        while(queue.TryDequeue(out var current))
        {
            if(visited.Contains(current)) continue;
            
            visited.Add(current);
            
            var pathCount = pathCountGrid.Get(current);
            
            Debug.Assert(pathCount > 0);
            
            grid.Set(current, '|');
            
            if(current.Y == grid.Height - 1)
            {
                continue;
            }
            
            var below = current.MoveY(1);
            
            // Split
            if(grid.Get(below) == '^')
            {
                pathCountGrid.Add(below.MoveX(-1), pathCount);
                pathCountGrid.Add(below.MoveX(1), pathCount);
                
                queue.Enqueue(below.MoveX(-1));
                queue.Enqueue(below.MoveX(1));
            }
            // Normal
            else
            {
                pathCountGrid.Add(below, pathCount);
                
                queue.Enqueue(below);
            }
        }
        
        return pathCountGrid.GetRow(pathCountGrid.Height - 1).Sum(x => x.Value);
    }
}