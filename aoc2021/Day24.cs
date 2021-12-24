namespace aoc2021;

/// <summary>
/// Day 24: <see href="https://adventofcode.com/2021/day/24"/>
/// </summary>
public sealed class Day24 : Day
{
    private readonly Dictionary<int, (int x, int y)> _keys = new();

    public Day24() : base(24, "Arithmetic Logic Unit")
    {
        var lines = Input.ToList();
        var pairs = Enumerable.Range(0, 14)
            .Select(i => (int.Parse(lines[i * 18 + 5][6..]), int.Parse(lines[i * 18 + 15][6..])))
            .ToList();

        var stack = new Stack<(int, int)>();
        foreach (var ((x, y), i) in pairs.Select((pair, i) => (pair, i)))
            if (x > 0)
                stack.Push((i, y));
            else
            {
                var (j, jj) = stack.Pop();
                _keys[i] = (j, jj + x);
            }
    }

    public override object Part1()
    {
        var output = new Dictionary<int, int>();

        foreach (var (key, (x, y)) in _keys)
        {
            output[key] = Math.Min(9, 9 + y);
            output[x] = Math.Min(9, 9 - y);
        }

        return long.Parse(string.Join("", output.OrderBy(x => x.Key).Select(x => x.Value)));
    }

    public override object Part2()
    {
        var output = new Dictionary<int, int>();

        foreach (var (key, (x, y)) in _keys)
        {
            output[key] = Math.Max(1, 1 + y);
            output[x] = Math.Max(1, 1 - y);
        }

        return long.Parse(string.Join("", output.OrderBy(x => x.Key).Select(x => x.Value)));
    }
}