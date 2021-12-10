namespace aoc2021;

public abstract class Day
{
    protected Day(int dayNumber, string puzzleName)
    {
        DayNumber = dayNumber;
        PuzzleName = puzzleName;
    }

    public static bool UseTestInput { get; set; }

    public int DayNumber { get; }
    public string PuzzleName { get; }

    protected IEnumerable<string> Input =>
        File.ReadLines(FileName);

    public string FileName =>
        Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
            $"input/{(UseTestInput ? "test" : "day")}{DayNumber,2:00}.in");

    public abstract string Part1();
    public abstract string Part2();

    public void AllParts(bool verbose = true)
    {
        Console.WriteLine($"Day {DayNumber,2}: {PuzzleName}");
        var s = Stopwatch.StartNew();
        var part1 = Part1();
        s.Stop();
        Console.Write($"Part 1: {part1,-25} ");
        Console.WriteLine(verbose ? $"{s.ScaleMilliseconds()}ms elapsed" : "");

        s.Reset();

        s.Start();
        var part2 = Part2();
        s.Stop();
        Console.Write($"Part 2: {part2,-25} ");
        Console.WriteLine(verbose ? $"{s.ScaleMilliseconds()}ms elapsed" : "");

        Console.WriteLine();
    }
}
