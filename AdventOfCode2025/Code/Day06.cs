using System.Text;
using System.Text.RegularExpressions;
using AdventOfCode2025.Util;

namespace AdventOfCode2025.Code;

public class Day06
{
    private Regex regex = new Regex(" +");
    
    public long RunA(string input)
    {
        var math = input
            .Split("\n")
            .Select(x => x.Trim())
            .Select(x => regex.Split(x).Select(x => x.Trim()).ToArray())
            .ToArray();
        
        int problemCount = math[0].Length;
        int numberCount = math.Length - 1;
        int operatorIndex = math.Length - 1;
        
        long total = 0;
        
        for(int problemIndex = 0; problemIndex < problemCount; problemIndex++)
        {
            var @operator = math[operatorIndex][problemIndex][0];
            
            var operands = new List<long>();
            
            for(int i = 0; i < numberCount; i++)
            {
                operands.Add(long.Parse(math[i][problemIndex]));
            }
            
            if(@operator == '+')
            {
                total += operands.Sum();
            }
            else if(@operator == '*')
            {
                total += operands.Aggregate(1L, (a, b) => a * b);
            }
            else
            {
                throw new Exception("Unexpected input");
            }
        }
        
        return total;
    }
    
    public long RunB(string input)
    {
        var grid = Grid<char>.Parse(input, c => c);
        
        long total = 0;
        
        foreach(var operatorLocation in GetOperator(grid))
        {
            var @operator = grid.Get(operatorLocation);
            
            var operands = GetOperands(grid, operatorLocation.X).ToArray();
            
            if(@operator == '+')
            {
                total += operands.Sum();
            }
            else if(@operator == '*')
            {
                total += operands.Aggregate(1L, (a, b) => a * b);
            }
            else
            {
                throw new Exception("Unexpected input");
            }
        }
        
        return total;
    }
    
    private IEnumerable<long> GetOperands(Grid<char> grid, int column)
    {
        string current = GetColumn(grid, column);
        
        while(current.Length > 0)
        {
            yield return long.Parse(current);
            
            column += 1;
            
            current = GetColumn(grid, column);
        }
    }
    
    private string GetColumn(Grid<char> grid, int column)
    {
        if(!grid.IsInBounds(column, 0))
        {
            return "";
        }
        
        StringBuilder builder = new StringBuilder();
        
        for(int i = 0; i < grid.Height - 1; i++)
        {
            if(grid.Get(column, i) != ' ')
            {
                builder.Append(grid.Get(column, i));
            }
        }
        
        return builder.ToString();
    }
    
    private IEnumerable<Point> GetOperator(Grid<char> grid)
    {
        for(int i = 0; i < grid.Width; i++ )
        {
            if(grid.Get(i, grid.Height - 1) != ' ') yield return new Point(i, grid.Height - 1);
        }
    }
}