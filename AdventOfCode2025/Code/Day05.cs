using System.Diagnostics;

namespace AdventOfCode2025.Code;

public class Day05
{
    record Range(long From, long To)
    {
        public bool Contains(long number)
        {
            // Inclusive ranges.
            if(number < From) return false;
            if(number > To) return false;
            return true;
        }
        
        public bool Overlaps(Range other)
        {
            if (To < other.From) return false;
            if (other.To < From) return false;
            
            return true;
        }
        
        public Range Merge(Range other)
        {
            Debug.Assert(Overlaps(other));
            Debug.Assert(other.Overlaps(this));
            
            return new Range(Math.Min(From, other.From), Math.Max(To, other.To));
        }
        
        public long Count => To - From + 1; // Inclusive ranges
    }
    
    public long RunA(string input)
    {
        var inputs = input.Split("\r\n\r\n");
        var ranges = inputs[0].Split("\n").Select(x => x.Split("-")).Select(x => x.Select(long.Parse).ToArray()).Select(x => new Range(x[0], x[1])).ToArray();
        var ids = inputs[1].Split("\n").Select(long.Parse).ToArray();
        
        return ids.Count(id => ranges.Any(range => range.Contains(id)));
    }
    
    public long RunB(string input)
    {
        var inputs = input.Split("\r\n\r\n");
        var ranges = inputs[0].Split("\n").Select(x => x.Split("-")).Select(x => x.Select(long.Parse).ToArray()).Select(x => new Range(x[0], x[1])).OrderBy(x => x.From).ToArray();
        
        var mergedRanges = new List<Range>();
        var currentRange = ranges[0];
        
        for(int i = 1; i < ranges.Length; i++)
        {
            var nextRange = ranges[i];
            
            if(!currentRange.Overlaps(nextRange))
            {
                mergedRanges.Add(currentRange);
                
                currentRange = nextRange;
                continue;
            }
            
            currentRange = currentRange.Merge(nextRange);
        }
        
        mergedRanges.Add(currentRange);
        
        return mergedRanges.Sum(x => x.Count);
    }
}