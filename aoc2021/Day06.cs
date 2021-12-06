namespace aoc2021;

/// <summary>
/// Day 6: <see href="https://adventofcode.com/2021/day/6"/>
/// </summary>
public sealed class Day06 : Day
{
    private readonly List<int> _fishes;
    public Day06() : base(6, "Lanternfish")
    {
        //UseTestInput = true;
        _fishes = Input.First().Split(',').Select(int.Parse).ToList();
    }

    private static List<int> DayStep(List<int> state)
    {
        List<int> result = new();
        
        foreach (var fish in state)
        {
            switch (fish)
            {
                case 0:
                    result.Add(6);
                    result.Add(8);
                    break;
                default:
                    result.Add(fish - 1);
                    break;
            }
        }

        return result;
    }

    public override string Part1()
    {
        var fishes = Enumerable.Range(0, 80).Aggregate(_fishes, (current, _) => DayStep(current));
        return $"{fishes.Count}";
    }

    public override string Part2() => "";
}
