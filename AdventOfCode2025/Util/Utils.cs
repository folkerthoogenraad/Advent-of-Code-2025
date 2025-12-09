namespace AdventOfCode2025.Util;

public static class Utils
{
    extension<T>(IEnumerable<T> enumerable)
    {
        public IEnumerable<(T From, T To)> Looped()
        {
            var enumerator = enumerable.GetEnumerator();
            
            if(!enumerator.MoveNext())
            {
                yield break;
            }
            
            var first = enumerator.Current;
            var previous = first;
            
            while(enumerator.MoveNext())
            {
                var current = enumerator.Current;
                
                yield return (previous, current);
                
                previous = current;
            }
            
            yield return (previous, first);
        }
    }
}