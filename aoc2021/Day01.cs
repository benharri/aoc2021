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

    public override string Part1()
    {
        var c = 0;
        for (var i = 0; i < _readings.Count - 1; i++)
        {
            if (_readings[i + 1] > _readings[i]) c++;
        }
        return $"{c}";
    }

    public override string Part2()
    {
        var c = 0;
        for (var i = 0; i < _readings.Count - 3; i++)
        {
            if (_readings[i + 3] > _readings[i]) c++;
        }
        return $"{c}";
    }
}
