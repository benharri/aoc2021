using System.Drawing;

namespace aoc2021;

/// <summary>
/// Day 5: <see href="https://adventofcode.com/2021/day/5"/>
/// </summary>
public sealed class Day05 : Day
{
    private readonly List<Line> _lines;
    
    public Day05() : base(5, "Hydrothermal Venture")
    {
        _lines = Input.Select(l => new Line(l)).ToList();
    }

    private class Line
    {
        public int X1 { get; }
        public int X2 { get; }
        public int Y1 { get; }
        public int Y2 { get; }

        public readonly List<Point> AllPoints = new();
        
        public Line(string line)
        {
            var s = line
                .Split(" -> ")
                .Select(i => i.Split(',').Select(int.Parse))
                .SelectMany(i => i)
                .ToList();
            
            X1 = s[0];
            X2 = s[1];
            Y1 = s[2];
            Y2 = s[3];

            if (X1 == X2)
            {
                var minY = Math.Min(Y1, Y2);
                var maxY = Math.Max(Y1, Y2);
                
                for (var i = 0; i < maxY - minY; i++)
                {
                    AllPoints.Add(new(minY + i, X1));
                }
            }
            else if (Y1 == Y2)
            {
                var minX = Math.Min(X1, X2);
                var maxX = Math.Max(X1, X2);

                for (var i = 0; i < maxX - minX; i++)
                {
                    AllPoints.Add(new(minX + i, Y1));
                }
            }
            else
            {
                var oX = X1 < X2 ? 1 : -1;
                var oY = Y1 < Y2 ? 1 : -1;
                for (var i = 0; i < Math.Abs(X1 - X2); i++)
                {
                    AllPoints.Add(new(X1 + i * oX, Y1 + i * oY));
                }
            }
        }
    }

    public override string Part1()
    {
        var groups = _lines
            .Where(l => l.X1 != l.X2 && l.Y1 != l.Y2)
            .SelectMany(l => l.AllPoints)
            .GroupBy(x => x);
            
        return $"{groups.Count(g => g.Count() > 1)}";
    }

    public override string Part2() => "";
}
