namespace aoc2021;

/// <summary>
/// Day 8: <see href="https://adventofcode.com/2021/day/8"/>
/// </summary>
public sealed class Day08 : Day
{
    public Day08() : base(8, "Seven Segment Search")
    {
    }

    public override string Part1() =>
        Input
            .Select(line => line.Split(" | ")[1].Split(' '))
            .Select(outputs => outputs.Count(o => new[] { 2, 3, 4, 7 }.Contains(o.Length)))
            .Sum()
            .ToString();

    public override string Part2() => "";
}
