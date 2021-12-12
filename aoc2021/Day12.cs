namespace aoc2021;

/// <summary>
/// Day 12: <see href="https://adventofcode.com/2021/day/12"/>
/// </summary>
public sealed class Day12 : Day
{
    private readonly Dictionary<string, List<string>> _edges = new();

    public Day12() : base(12, "Passage Pathing")
    {
        foreach (var line in Input)
        {
            var s = line.Split('-', 2);

            if (_edges.ContainsKey(s[0])) _edges[s[0]].Add(s[1]);
            else _edges[s[0]] = new() { s[1] };

            if (_edges.ContainsKey(s[1])) _edges[s[1]].Add(s[0]);
            else _edges[s[1]] = new() { s[0] };
        }
    }

    private static int WalkGraph(IReadOnlyDictionary<string, List<string>> edges, string point,
        Dictionary<string, bool> seen)
    {
        if (point == "end") return 1;
        if (char.IsLower(point[0]) && seen.GetValueOrDefault(point, false)) return 0;

        seen[point] = true;
        return edges[point].Sum(path => WalkGraph(edges, path, seen.ToDictionary(k => k.Key, v => v.Value)));
    }

    private static int TraverseGraph(IReadOnlyDictionary<string, List<string>> edges, string point,
        Dictionary<string, bool> seen)
    {
        if (point == "end") return 1;
        if (!TwiceCheck(point, seen)) return 0;

        seen[point] = true;
        return edges[point].Sum(path => TraverseGraph(edges, path, seen.ToDictionary(k => k.Key, v => v.Value)));
    }

    private static bool TwiceCheck(string point, Dictionary<string, bool> seen)
    {
        if (point == "start" && seen.GetValueOrDefault(point, false))
            return false;
        if (!char.IsLower(point[0]) || !seen.GetValueOrDefault(point, false))
            return true;
        if (seen.GetValueOrDefault("_twice", false))
            return false;
        
        seen["_twice"] = true;
        return true;
    }

    public override string Part1() =>
        $"{WalkGraph(_edges, "start", new())}";

    public override string Part2() =>
        $"{TraverseGraph(_edges, "start", new())}";
}