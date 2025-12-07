using System.Numerics;

namespace AdventOfCode2025.Util;

public class Grid<T>
{
    public int Width {get; set;}
    public int Height {get; set;}
    public T[] Data {get ;set; }

    public Grid(int width, int height)
    {
        Width = width;
        Height = height;
        Data = new T[width * height];
    }

    public Grid<T> Clone(){
        var grid = new Grid<T>(Width, Height);

        for(int i = 0; i < Data.Length; i++){
            grid.Data[i] = Data[i];
        }

        return grid;
    }
    
    public Grid<TOther> Map<TOther>(Func<Point, T, TOther> converter)
    {
        var grid = new Grid<TOther>(Width, Height);
        
        for(int x = 0; x < Width; x++) {
            for(int y = 0; y < Height; y++){
                var point = new Point(x, y);
                
                grid.Set(point, converter(point, Get(point)));
            }
        }
        
        return grid;
    }

    public void Set(int x, int y, T value){
        Data[IndexOf(x, y)] = value;
    }

    public void Fill(IEnumerable<Point> points, T value){
        foreach (var point in points)
        {
            Set(point, value);
        }
    }

    public void Foreach(Action<Point, T> action)
    {
        for(int x = 0; x < Width; x++) {
            for(int y = 0; y < Height; y++){
                action(new Point(x, y), Get(x, y));
            }
        }
    }
    
    public Point FindRequired(Func<T, bool> predicate)
    {
        var point = Find(predicate);
        
        if(!point.HasValue)
        {
            throw new Exception("Point couln't be found ");
        }
        
        return point.Value;
    }

    public Point? Find(Func<T, bool> predicate)
    {
        for(int x = 0; x < Width; x++) {
            for(int y = 0; y < Height; y++){
                if(predicate(Get(x, y))){
                    return new Point(x, y);
                }
            }
        }

        return null;
    }
    
    public IEnumerable<Point> FindAll(Func<T, bool> predicate)
    {
        for(int x = 0; x < Width; x++) {
            for(int y = 0; y < Height; y++){
                if(predicate(Get(x, y))){
                    yield return new Point(x, y);
                }
            }
        }
    }
    
    public IEnumerable<Point> AllPoints
    {
        get
        {
            for(int x = 0; x < Width; x++) {
                for(int y = 0; y < Height; y++){
                    yield return new Point(x, y);
                }
            }
        }
    }
    
    public IEnumerable<int> Rows
    {
        get
        {
            for(int y = 0; y < Height; y++)
            {
                yield return y;
            }
        }
    }
    
    public IEnumerable<int> Columns
    {
        get
        {
            for(int x = 0; x < Width; x++)
            {
                yield return x;
            }
        }
    }

    public int IndexOf(int x, int y){
        if(!IsInBounds(x, y)) throw new IndexOutOfRangeException($"{x}, {y} is not in bounds of grid ({Width}, {Height})");
        return x + y * Width;
    }

    public int IndexOf(Point p){
        return IndexOf(p.X, p.Y);
    }

    public void Set(Point p, T value){
        Set(p.X, p.Y, value);
    }
    
    public bool IsInBounds(Point p){
        return IsInBounds(p.X, p.Y);
    }
    public bool IsInBounds(int x, int y){
        if(x < 0) return false;
        if(x >= Width) return false;
        if(y < 0) return false;
        if(y >= Height) return false;

        return true;
    }

    public T Get(int x, int y){
        return Data[IndexOf(x, y)];
    }
    public T Get(Point p){
        return Data[IndexOf(p)];
    }

    public static Grid<T> Parse(string text, Func<char, T> converter) {
        string[] lines = text
            .Replace("\r", "")
            .Split('\n');

        int height = lines.Length;
        int width = lines[0].Length;

        var grid = new Grid<T>(width, height);

        for(int y = 0; y < height; y++) {
            for(int x =0; x < width; x++){
                grid.Set(x, y, converter(lines[y][x]));
            }
        }

        return grid;
    }
}

public static class GridExtensions {
    public static void Print(this Grid<char> grid){
        for(int y = 0; y < grid.Height; y++){
            for(int x = 0; x < grid.Width; x++){
                Console.Write(grid.Get(x, y));
            }
            Console.WriteLine();
        }
    }
    
    public static void Print<T>(this Grid<T> grid, Func<T, char> converter)
    {
        for(int y = 0; y < grid.Height; y++){
            for(int x = 0; x < grid.Width; x++){
                Console.Write(converter(grid.Get(x, y)));
            }
            Console.WriteLine();
        }
    }
    
    public static void Add<T>(this Grid<T> grid, Point p, T value) 
        where T :  INumber<T> 
    {
        var current = grid.Get(p);
                
        grid.Set(p, current + value);
    }
    
    
    public static IEnumerable<(Point Point, T Value)> GetRow<T>(this Grid<T> grid, int row) 
        where T :  INumber<T> 
    {
        for(int i = 0; i < grid.Width; i++)
        {
            var point = new Point(i, row);
            
            yield return (point, grid.Get(point));
        }
    }
}