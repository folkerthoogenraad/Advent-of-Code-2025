namespace AdventOfCode2025.Code;

public class Day03
{
    public long RunA(string input)
    {
        long totalJolt = 0;
        
        foreach(var line in input.Split("\n").Select(x => x.Trim()))
        {
            int firstIndex = GetHighestNumberIndex(line, 0, line.Length - 1);
            int secondIndex = GetHighestNumberIndex(line, firstIndex + 1, line.Length);
            
            long jolt = ToInt(line[firstIndex]) * 10 + ToInt(line[secondIndex]);
            
            Console.WriteLine(line + " has a jolt of " + jolt);
            
            totalJolt += jolt;
        }
        
        return totalJolt;
    }
    
    public long RunB(string input)
    {
        long totalJolt = 0;
        
        foreach(var line in input.Split("\n").Select(x => x.Trim()))
        {
            long jolt = 0;
            
            int startIndex = 0;
            
            for(int i = 0; i < 12; i ++)
            {
                int index = GetHighestNumberIndex(line, startIndex, line.Length - (11 - i));
                
                startIndex = index + 1;
                
                long n = ToInt(line[index]);
                
                int m = 11 - i;
                
                while(m > 0)
                {
                    n *= 10;
                    m -= 1;
                }
                
                jolt += n;
            }
            
            totalJolt += jolt;
        }
        
        return totalJolt;
    }
    
    public static int ToInt(char c)
    {
        return c - '0';
    }
    
    public static int GetHighestNumberIndex(string input, int start, int end)
    {
        // Console.Write("Searching for highest in " + input.Substring(start, end - start) + " - ");
        
        int index = -1;
        char highest = '/';
        
        for(int i = start; i < end; i++)
        {
            // Console.Write(input[i]);
            if(input[i] > highest)
            {
                index = i;
                highest = input[i];
            }
        }
        
        // Console.WriteLine();
        return index;
    }
}