namespace aoc2021;

/// <summary>
/// Day 1: <see href="https://adventofcode.com/2021/day/1"/>
/// </summary>
public sealed class Day01 : Day
{
    private readonly List<int> _readings;
    
    public Day01() : base(1, "Sonar Sweep")
    {
        _readings = Input.Select(int.Parse).ToList();
    }

    public override object Part1() =>
        Enumerable.Range(0, _readings.Count - 1).Count(i => _readings[i + 1] > _readings[i]);

    public override object Part2() =>
        Enumerable.Range(0, _readings.Count - 3).Count(i => _readings[i + 3] > _readings[i]);
}
