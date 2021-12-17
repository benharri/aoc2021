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

    public override object Part2()
    {
        var successfulVelocities = new HashSet<(int x, int y)>();
        var xMin = 1;
        while (xMin * (xMin + 1) / 2 < _target[0]) xMin++;

        for (var x = xMin; x <= _target[1]; x++)
        for (var y = _target[2]; y < Math.Abs(_target[2]); y++)
        {
            int xVelocity = x, yVelocity = y, xPos = 0, yPos = 0;

            while (xPos <= _target[1] && yPos >= _target[2])
            {
                xPos += xVelocity;
                yPos += yVelocity;

                if (xPos >= _target[0] && xPos <= _target[1] && yPos >= _target[2] && yPos <= _target[3])
                    successfulVelocities.Add((x, y));

                xVelocity = xVelocity == 0 ? 0 : xVelocity - 1;
                yVelocity--;
            }
        }

        return successfulVelocities.Count;
    }
}