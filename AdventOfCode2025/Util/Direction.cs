namespace AdventOfCode2025.Util;

public enum Direction { Up, Down, Left, Right }

public static class DirectionExtensions {
    
    
    public static Direction ToDirection(this char c)
    {
        return c switch
        {
            '^' => Direction.Up,
            '>' => Direction.Right,
            '<' => Direction.Left,
            'v' => Direction.Down,
            _ => throw new Exception($"Unknown direction '{c}'")
        };
    }
    public static Direction Reverse(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Down,
            Direction.Right => Direction.Left,
            Direction.Down => Direction.Up,
            Direction.Left => Direction.Right,
            _ => throw new Exception($"Unknown direction '{dir}'")
        };
    }
    
    public static Direction Rotate(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => Direction.Left,
            Direction.Left => Direction.Down,
            Direction.Down => Direction.Right,
            Direction.Right => Direction.Up,
            _ => throw new Exception($"Unknown direction '{dir}'")
        };
    }
    
    public static char ToCharacter(this Direction dir)
    {
        return dir switch
        {
            Direction.Up => '^',
            Direction.Left => '<',
            Direction.Down => 'v',
            Direction.Right => '>',
            _ => throw new Exception($"Unknown direction '{dir}'")
        };
    }
    public static string ToCharacterString(this Direction dir)
    {
        return dir.ToCharacter().ToString();
    }
    public static Point GetDirection(this Direction direction)
    {
        return direction switch
        {
            Direction.Up => new Point(0, -1),
            Direction.Down => new Point(0, 1),
            Direction.Right => new Point(1, 0),
            Direction.Left => new Point(-1, 0),
            _ => throw new Exception("")
        };
    }
}