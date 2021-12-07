namespace aoc2021;

/// <summary>
/// Day 7: <see href="https://adventofcode.com/2021/day/7"/>
/// </summary>
public sealed class Day07 : Day
{
    private readonly List<int> _tape;
    
    public Day07() : base(7, "The Treachery of Whales")
    {
        _tape = Input.First().Split(',').Select(int.Parse).OrderBy(i => i).ToList();
    }

    private static int ArithmeticSumTo(int n) => n * (n + 1) / 2;

    public override string Part1()
    {
        var i = _tape[_tape.Count / 2];
        return $"{_tape.Select(t => Math.Abs(t - i)).Sum()}";
    }

    public override string Part2()
    {
        var avg = (decimal)_tape.Sum() / _tape.Count;
        var floor = _tape.Select(t => ArithmeticSumTo(Math.Abs(t - (int)Math.Floor(avg)))).Sum();
        var ceil = _tape.Select(t => ArithmeticSumTo(Math.Abs(t - (int)Math.Ceiling(avg)))).Sum();
        return $"{Math.Min(floor, ceil)}";
    }
}
