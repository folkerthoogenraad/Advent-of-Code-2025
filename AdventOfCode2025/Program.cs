using AdventOfCode2025.Code;
using AdventOfCode2025.Util;

// Generates the input and example files automatically :)
// Just convenient for filling them out that you don't have to create them
// but you still have to fill them, so it doesn't really help all that much.
for(int i = 1; i <= 25; i++)
{
    var regular = $"input_2025_{i:d2}.txt";
    var example = $"input_2025_{i:d2}_example.txt";
    
    if(!File.Exists(regular)) { File.WriteAllText(regular, ""); }
    if(!File.Exists(example)) { File.WriteAllText(example, ""); }
}

// Console.WriteLine("Day01 A : " + new Day01().RunA(File.ReadAllText("input_2025_01.txt")));
// Console.WriteLine("Day01 B : " + new Day01().RunB(File.ReadAllText("input_2025_01.txt")));
// Console.WriteLine("Day02 A : " + new Day02().RunA(File.ReadAllText("input_2025_02.txt")));
// Console.WriteLine("Day02 B : " + new Day02().RunB(File.ReadAllText("input_2025_02.txt")));
// Console.WriteLine("Day03 A : " + new Day03().RunA(File.ReadAllText("input_2025_03.txt")));
// Console.WriteLine("Day03 B : " + new Day03().RunB(File.ReadAllText("input_2025_03.txt")));
// Console.WriteLine("Day04 A : " + new Day04().RunA(File.ReadAllText("input_2025_04.txt")));
// Console.WriteLine("Day04 B : " + new Day04().RunB(File.ReadAllText("input_2025_04.txt")));
// Console.WriteLine("Day05 A : " + new Day05().RunA(File.ReadAllText("input_2025_05.txt")));
// Console.WriteLine("Day05 B : " + new Day05().RunB(File.ReadAllText("input_2025_05.txt")));
// Console.WriteLine("Day06 A : " + new Day06().RunA(File.ReadAllText("input_2025_06.txt")));
// Console.WriteLine("Day06 B : " + new Day06().RunB(File.ReadAllText("input_2025_06.txt")));
// Console.WriteLine("Day07 A : " + new Day07().RunA(File.ReadAllText("input_2025_07.txt")));
// Console.WriteLine("Day07 B : " + new Day07().RunB(File.ReadAllText("input_2025_07.txt")));
// Console.WriteLine("Day08 A : " + new Day08().RunA(File.ReadAllText("input_2025_08.txt")));
// Console.WriteLine("Day08 B : " + new Day08().RunB(File.ReadAllText("input_2025_08.txt")));

// Console.WriteLine("Day09 A : " + new Day09().RunA(File.ReadAllText("input_2025_09.txt")));
// Console.WriteLine("Day09 B : " + new Day09().RunB(File.ReadAllText("input_2025_09.txt")));
