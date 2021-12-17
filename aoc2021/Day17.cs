namespace aoc2021;

/// <summary>
/// Day 17: <see href="https://adventofcode.com/2021/day/17"/>
/// </summary>
public sealed class Day17 : Day
{
    private readonly List<int> _target;
    
    public Day17() : base(17, "Trick Shot")
    {
        _target = Input.First()
            .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
            .Skip(2)
            .SelectMany(i => i.Split('=')[1].Split(".."))
            .Select(i => i.TrimEnd(','))
            .Select(int.Parse)
            .ToList();
    }

    public override object Part1()
    {
        var initialYVelocity = Math.Abs(_target[2]) - 1;
        return (initialYVelocity + 1) * initialYVelocity / 2;
    }

    public override object Part2() => "";
}
