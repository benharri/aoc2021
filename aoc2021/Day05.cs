namespace aoc2021;

/// <summary>
/// Day 5: <see href="https://adventofcode.com/2021/day/5"/>
/// </summary>
public sealed class Day05 : Day
{
    public Day05() : base(5, "Hydrothermal Venture")
    {
    }

    private int Solve(bool diagonals = false) =>
        Input
            .Where(s => !string.IsNullOrEmpty(s))
            .Select(s => s.Split(" -> "))
            .Select(a => a.Select(i => i.Split(',')).SelectMany(i => i.Select(int.Parse)).ToList())
            .Where(t => diagonals || t[0] == t[2] || t[1] == t[3])
            .SelectMany(t =>
                Enumerable.Range(0, Math.Max(Math.Abs(t[0] - t[2]), Math.Abs(t[1] - t[3])) + 1)
                    .Select(i => (
                        t[0] > t[2] ? t[2] + i : t[0] < t[2] ? t[2] - i : t[2],
                        t[1] > t[3] ? t[3] + i : t[1] < t[3] ? t[3] - i : t[3])))
            .GroupBy(k => k)
            .Count(k => k.Count() > 1);

    public override string Part1() => $"{Solve()}";

    public override string Part2() => $"{Solve(diagonals: true)}";
}
