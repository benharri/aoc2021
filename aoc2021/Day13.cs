using MoreLinq;

namespace aoc2021;

/// <summary>
/// Day 13: <see href="https://adventofcode.com/2021/day/13"/>
/// </summary>
public sealed class Day13 : Day
{
    private List<(int x, int y)> _dots;
    private readonly List<(char axis, int index)> _folds;
    
    public Day13() : base(13, "Transparent Origami")
    {
        var s = Input.Split("").ToList();
        
        _dots = s[0].Select(p =>
        {
            var i = p.Split(',', 2).Select(int.Parse).ToList();
            return (i[0], i[1]);
        }).ToList();
        
        _folds = s[1].Select(p => p.Split(' ').Skip(2).First()).Select(p =>
        {
            var i = p.Split('=', 2);
            return (i[0][0], int.Parse(i[1]));
        }).ToList();
    }
    
    private static List<(int x, int y)> DoFold(List<(int x, int y)> grid, char axis, int at)
    {
        List<(int, int)> result = new();
        
        switch (axis)
        {
            case 'x':
                foreach (var (x, y) in grid) result.Add((x > at ? 2 * at - x : x, y));
                break;
            case 'y':
                foreach (var (x, y) in grid) result.Add((x, y > at ? 2 * at - y : y));
                break;
            default: throw new ArgumentException("invalid fold axis", nameof(axis));
        }

        return result.Distinct().ToList();
    }

    private string PrintGrid()
    {
        var xMax = _dots.Max(g => g.x);
        var yMax = _dots.Max(g => g.y);
        var s = new StringBuilder();

        for (var y = 0; y <= yMax; y++)
        {
            for (var x = 0; x <= xMax; x++)
                s.Append(_dots.Contains((x, y)) ? "█" : "▒");

            s.AppendLine();
        }

        return s.ToString();
    }

    public override object Part1() =>
        DoFold(_dots, _folds[0].axis, _folds[0].index).Count;

    public override object Part2()
    {
        foreach (var (axis, at) in _folds)
            _dots = DoFold(_dots, axis, at);

        return Environment.NewLine + PrintGrid();
    }
}