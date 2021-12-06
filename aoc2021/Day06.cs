namespace aoc2021;

/// <summary>
/// Day 6: <see href="https://adventofcode.com/2021/day/6"/>
/// </summary>
public sealed class Day06 : Day
{
    private readonly long _p1, _p2;

    public Day06() : base(6, "Lanternfish")
    {
        var fishes = Input.First().Split(',').Select(long.Parse).ToList();
        Dictionary<long, long> counts = new();
        
        for (var i = 0; i <= 8; i++)
            counts[i] = fishes.Count(x => x == i);
        
        for (var i = 0; i < 256; i++)
        {
            var breeders = counts[0];
            for (var j = 0; j < 8; j++)
                counts[j] = counts[j + 1];
            counts[6] += breeders;
            counts[8] = breeders;

            if (i == 79) _p1 = counts.Values.Sum();
        }

        _p2 = counts.Values.Sum();
    }

    public override string Part1() => $"{_p1}";

    public override string Part2() => $"{_p2}";
}
