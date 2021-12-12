using MoreLinq;

namespace aoc2021;

/// <summary>
/// Day 9: <see href="https://adventofcode.com/2021/day/9"/>
/// </summary>
public sealed class Day09 : Day
{
    private readonly int _part1Sum;
    private readonly List<(int x, int y)> _lowPoints;
    private readonly List<string> _map;

    public Day09() : base(9, "Smoke Basin")
    {
        _part1Sum = 0;
        _lowPoints = new();
        _map = Input.ToList();

        for (var y = 0; y < _map.Count; y++)
        for (var x = 0; x < _map[y].Length; x++)
        {
            var c = _map[y][x];
            if (y > 0 && _map[y - 1][x] <= c) continue;
            if (y < _map.Count - 1 && _map[y + 1][x] <= c) continue;
            if (x > 0 && _map[y][x - 1] <= c) continue;
            if (x < _map[y].Length - 1 && _map[y][x + 1] <= c) continue;

            _lowPoints.Add((x, y));
            _part1Sum += c - '0' + 1;
        }
    }

    public override object Part1() => _part1Sum;

    public override object Part2()
    {
        var sizes = new List<long>();
        foreach (var (x, y) in _lowPoints)
        {
            var s = 0;
            var seen = new HashSet<(int x, int y)>();

            IEnumerable<(int x, int y)> Traverse((int x, int y) p)
            {
                var (i, j) = p;
                if (_map[j][i] == '9') yield break;
                if (seen.Contains((i, j))) yield break;
                seen.Add((i, j));
                s++;
                
                if (j > 0)
                    yield return (i, j - 1);
                if (j < _map.Count - 1)
                    yield return (i, j + 1);
                if (i > 0)
                    yield return (i - 1, j);
                if (i < _map[j].Length - 1)
                    yield return (i + 1, j);
            }

            MoreEnumerable.TraverseBreadthFirst((x, y), Traverse).Consume();
            sizes.Add(s);
        }

        return sizes
            .OrderByDescending(x => x)
            .Take(3)
            .Aggregate(1L, (a, b) => a * b);
    }
}
