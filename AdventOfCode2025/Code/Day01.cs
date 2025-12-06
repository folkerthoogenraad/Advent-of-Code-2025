namespace AdventOfCode2025.Code;

public class Day01
{
    public int RunA(string input)
    {
        int count = 0;
        int currentValue = 50;

        foreach (var line in input.Split("\n").Select(x => x.Trim()))
        {
            char dir = line[0];
            int n = int.Parse(line.Substring(1));

            if (dir == 'L') currentValue -= n;
            else if (dir == 'R') currentValue += n;
            else throw new Exception("Invalid input");

            currentValue = currentValue % 100;
            if (currentValue < 0) currentValue += 100;

            if (currentValue == 0) count += 1;
        }

        return count;
    }
    
    public class Dial
    {
        public int Count = 0;
        public int CurrentValue = 50;
        
        public void MoveLeft()
        {
            CurrentValue -= 1;
            
            if(CurrentValue < 0)
            {
                CurrentValue += 100;
            }
            
            if(CurrentValue == 0)
            {
                Count += 1;
            }
        }
        
        public void MoveRight()
        {
            CurrentValue += 1;
            
            if(CurrentValue > 99)
            {
                CurrentValue -= 100;
            }
            
            if(CurrentValue == 0)
            {
                Count += 1;
            }
        }
    }
    
    public int RunB2(string input)
    {
        int count = 0;
        int currentValue = 50;

        foreach (var line in input.Split("\n").Select(x => x.Trim()))
        {
            char dir = line[0];
            int n = int.Parse(line.Substring(1));

            if (dir == 'L') currentValue -= n;
            else if (dir == 'R') currentValue += n;
            else throw new Exception("Invalid input");
            
            // This does some strange overcounting, not sure, whatever.
            while (currentValue >= 100) { currentValue -= 100; count += 1; }
            while (currentValue < 0) { currentValue += 100; count += 1;  }
            
            if (currentValue == 0) count += 1;
        }

        return count;
    }
    
    public int RunB(string input)
    {
        var dial = new Dial();

        foreach (var line in input.Split("\n").Select(x => x.Trim()))
        {
            char dir = line[0];
            int n = int.Parse(line.Substring(1));

            for(int i = 0; i < n; i++)
            {
                if (dir == 'L') dial.MoveLeft();
                else if (dir == 'R') dial.MoveRight();
                else throw new Exception("Invalid input");
            }
        }

        return dial.Count;
    }
}