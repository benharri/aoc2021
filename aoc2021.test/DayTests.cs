namespace aoc2021.test;

[TestClass]
public class DayTests
{
    [DataTestMethod]
    [DataRow(typeof(Day01), "", "")]
    public void CheckAllDays(Type dayType, string part1, string part2)
    {
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1();
        s.Stop();
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in part1");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2();
        s.Stop();
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in part2");
        Assert.AreEqual(part2, part2Actual, $"Incorrect answer for Day {day.DayNumber} Part2");
    }
}
