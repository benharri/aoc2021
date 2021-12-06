namespace aoc2021.test;

[TestClass]
public class DayTests
{
    [DataTestMethod]
    [DataRow(typeof(Day01), "1616", "1645")]
    [DataRow(typeof(Day02), "2272262", "2134882034")]
    [DataRow(typeof(Day03), "3009600", "6940518")]
    [DataRow(typeof(Day04), "8580", "9576")]
    [DataRow(typeof(Day05), "7318", "19939")]
    [DataRow(typeof(Day06), "362740", "1644874076764")]
    public void CheckAllDays(Type dayType, string part1, string part2)
    {
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Assert.IsTrue(File.Exists(day.FileName));
        Console.Write($"Day {day.DayNumber}: {day.PuzzleName,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1();
        s.Stop();
        Console.Write($"Part1: {part1Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2();
        s.Stop();
        Console.Write($"Part2: {part2Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part2, part2Actual, $"Incorrect answer for Day {day.DayNumber} Part2");
    }
    
    [DataTestMethod]
    [DataRow(typeof(Day01), "7", "5")]
    [DataRow(typeof(Day02), "150", "900")]
    [DataRow(typeof(Day03), "198", "230")]
    [DataRow(typeof(Day04), "4512", "1924")]
    [DataRow(typeof(Day05), "5", "12")]
    [DataRow(typeof(Day06), "5934", "26984457539")]
    public void CheckTestInputs(Type dayType, string part1, string part2)
    {
        Day.UseTestInput = true;
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Assert.IsTrue(File.Exists(day.FileName));
        Console.Write($"Day {day.DayNumber}: {day.PuzzleName,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1();
        s.Stop();
        Console.Write($"Part1: {part1Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2();
        s.Stop();
        Console.Write($"Part2: {part2Actual,-15} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part2, part2Actual, $"Incorrect answer for Day {day.DayNumber} Part2");
    }
}
