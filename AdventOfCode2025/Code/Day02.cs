namespace AdventOfCode2025.Code;

public class Day02
{
    record Range(long From, long To)
    {
    }
    
    public long RunA(string input)
    {
        var ranges = input.Split(',').Select(s => s.Split('-')).Select(s => new Range(long.Parse(s[0]), long.Parse(s[1])));
        
        long sum = 0;
        
        foreach(var range in ranges)
        {
            for(long i = range.From; i <= range.To; i++)
            {
                string s = i.ToString();
                
                if(s.Length % 2 != 0) continue;
                
                string left = s.Substring(0, s.Length / 2);
                string right = s.Substring(s.Length / 2);
                
                if(left == right) sum += i;
            }
        }
        
        return sum;
    }
    
    public long RunB(string input)
    {
        // This may be one of my more ugly programs ive ever written.
        var ranges = input.Split(',').Select(s => s.Split('-')).Select(s => new Range(long.Parse(s[0]), long.Parse(s[1])));
        
        long sum = 0;
        
        foreach(var range in ranges)
        {
            for(long i = range.From; i <= range.To; i++)
            {
                string s = i.ToString();
                
                // Not even be smart about it, just try 100 digits, because I'm too stupid to figure out 
                // a simpler way
                for(int div = 2; div < 100; div++)
                {
                    if(s.Length % div != 0) continue;
                    
                    int substringLength = s.Length / div;
                    
                    string first = s.Substring(0, substringLength);
                    
                    bool equal = true;
                    
                    for(int index = 1; index < div; index++)
                    {
                        string next = s.Substring(index * substringLength, substringLength);
                        
                        if(first != next)
                        {
                            equal = false;
                            break;
                        }
                    } 
                    
                    if(equal)
                    {
                        sum += i;
                        break;
                    }
                }
            }
        }
        
        return sum;
    }
}