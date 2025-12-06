using AdventOfCode2025.Util;

namespace AdventOfCode2025.Code;

public class Day04
{
    public long RunA(string input)
    {
        long total = 0;
        
        var grid = Grid<char>.Parse(input, c => c);
        var writeGrid = grid.Clone();
        
        foreach(var target in grid.AllPoints)
        {
            if(grid.Get(target) != '@') continue;
            
            var adjecentCount = Point.Rectangle(target.Move(-1, -1), 3, 3)
                .Where(x => x != target)
                .Where(x => grid.IsInBounds(x))
                .Where(x => grid.Get(x) == '@')
                .Count();
            
            if(adjecentCount < 4)
            {
                total += 1;
                writeGrid.Set(target, 'x');
            }
            else
            {
                writeGrid.Set(target, '+');
            }
        }
        
        writeGrid.Print();
        
        return total;
    }
    
    public long RunB(string input)
    {
        long total = 0;
        
        var grid = Grid<char>.Parse(input, c => c);
        
        int removedCount = 0;
        
        do
        {
            removedCount = 0;
            
            foreach(var target in grid.AllPoints)
            {
                if(grid.Get(target) != '@') continue;
            
                var adjecentCount = Point.Rectangle(target.Move(-1, -1), 3, 3)
                    .Where(x => x != target)
                    .Where(x => grid.IsInBounds(x))
                    .Where(x => grid.Get(x) == '@')
                    .Count();
            
                if(adjecentCount < 4)
                {
                    total += 1;
                    grid.Set(target, '.');
                    removedCount += 1;
                }
            }
        }
        while(removedCount > 0);
        
        grid.Print();
        
        return total;
    }
}