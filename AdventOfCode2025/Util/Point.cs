namespace AdventOfCode2025.Util;

public record struct Point(int X, int Y)
{
    public static Point Up => new(0, -1);
    public static Point Down => new(0, 1);
    public static Point Left => new(-1, 0);
    public static Point Right => new(1, 0);
    
    public Point Perpendicular()
    {
        return new Point(-Y, X);
    }
    
    public static Point operator-(Point a){
        return new Point(-a.X, -a.Y);
    }
    public static Point operator+(Point a, Point b){
        return new Point(a.X + b.X, a.Y + b.Y);
    }
    public static Point operator-(Point a, Point b){
        return new Point(a.X - b.X, a.Y - b.Y);
    }

    public static Point operator*(Point a, int b){
        return new Point(a.X * b, a.Y * b);
    }
    
    public Point MoveX(int x)
    {
        return new Point(X + x, Y);
    }
    
    public Point MoveY(int y)
    {
        return new Point(X, Y + y);
    }
    
    public Point Move(int x, int y)
    {
        return new Point(X + x, Y + y);
    }
    
    public static IEnumerable<Point> Rectangle(Point start, int width, int height)
    {
        for(int i = 0; i < width; i ++)
        {
            for(int j = 0; j < height; j++)
            {
                yield return start + new Point(i, j);
            }
        }
    }
    
    public static IEnumerable<Point> Rectangle(int x, int y, int width, int height)
    {
        return Rectangle(new Point(x, y), width, height);
    }
}

public record struct PointL(long X, long Y)
{
    public static PointL operator+(PointL a, PointL b){
        return new PointL(a.X + b.X, a.Y + b.Y);
    }
    public static PointL operator-(PointL a, PointL b){
        return new PointL(a.X - b.X, a.Y - b.Y);
    }
    public static PointL operator*(PointL a, long b){
        return new PointL(a.X * b, a.Y * b);
    }
}