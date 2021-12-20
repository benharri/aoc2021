namespace aoc2021.test;

[TestClass]
public class DayTests
{
    private const string Day13Actual = @"
████▒███▒▒████▒▒██▒▒█▒▒█▒▒██▒▒█▒▒█▒█▒▒█
█▒▒▒▒█▒▒█▒▒▒▒█▒█▒▒█▒█▒█▒▒█▒▒█▒█▒▒█▒█▒▒█
███▒▒█▒▒█▒▒▒█▒▒█▒▒▒▒██▒▒▒█▒▒▒▒████▒█▒▒█
█▒▒▒▒███▒▒▒█▒▒▒█▒██▒█▒█▒▒█▒▒▒▒█▒▒█▒█▒▒█
█▒▒▒▒█▒▒▒▒█▒▒▒▒█▒▒█▒█▒█▒▒█▒▒█▒█▒▒█▒█▒▒█
████▒█▒▒▒▒████▒▒███▒█▒▒█▒▒██▒▒█▒▒█▒▒██▒
";

    private const string Day13Test = @"
█████
█▒▒▒█
█▒▒▒█
█▒▒▒█
█████
";

    [DataTestMethod]
    [DataRow(typeof(Day01), "1616", "1645")]
    [DataRow(typeof(Day02), "2272262", "2134882034")]
    [DataRow(typeof(Day03), "3009600", "6940518")]
    [DataRow(typeof(Day04), "8580", "9576")]
    [DataRow(typeof(Day05), "7318", "19939")]
    [DataRow(typeof(Day06), "362740", "1644874076764")]
    [DataRow(typeof(Day07), "345035", "97038163")]
    [DataRow(typeof(Day08), "362", "1020159")]
    [DataRow(typeof(Day09), "478", "1327014")]
    [DataRow(typeof(Day10), "288291", "820045242")]
    [DataRow(typeof(Day11), "1613", "510")]
    [DataRow(typeof(Day12), "4549", "120535")]
    [DataRow(typeof(Day13), "837", Day13Actual)]
    [DataRow(typeof(Day14), "5656", "12271437788530")]
    [DataRow(typeof(Day15), "702", "2955")]
    [DataRow(typeof(Day16), "852", "19348959966392")]
    [DataRow(typeof(Day17), "12090", "5059")]
    [DataRow(typeof(Day18), "4289", "4807")]
    // [DataRow(typeof(Day19), "338", "9862")] // takes too long and i don't feel like optimizing
    [DataRow(typeof(Day20), "5306", "17497")]
    public void CheckAllDays(Type dayType, string part1, string part2)
    {
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Assert.IsTrue(File.Exists(day.FileName));
        Console.Write($"Day {day.DayNumber,2}: {day.PuzzleName,-25} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1().ToString();
        s.Stop();
        Console.Write($"Part 1: {part1Actual,-25} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2().ToString();
        s.Stop();
        Console.Write($"Part 2: {part2Actual,-25} ");
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
    [DataRow(typeof(Day07), "37", "168")]
    [DataRow(typeof(Day08), "26", "61229")]
    [DataRow(typeof(Day09), "15", "1134")]
    [DataRow(typeof(Day10), "26397", "288957")]
    [DataRow(typeof(Day11), "1656", "195")]
    [DataRow(typeof(Day12), "226", "3509")]
    [DataRow(typeof(Day13), "17", Day13Test)]
    [DataRow(typeof(Day14), "1588", "2188189693529")]
    [DataRow(typeof(Day15), "40", "315")]
    [DataRow(typeof(Day16), "16", "15")]
    [DataRow(typeof(Day17), "45", "112")]
    [DataRow(typeof(Day18), "4140", "3993")]
    [DataRow(typeof(Day19), "79", "3621")]
    [DataRow(typeof(Day20), "35", "3351")]
    public void CheckTestInputs(Type dayType, string part1, string part2)
    {
        Day.UseTestInput = true;
        var s = Stopwatch.StartNew();
        var day = Activator.CreateInstance(dayType) as Day;
        s.Stop();
        Assert.IsNotNull(day, "failed to instantiate day object");
        Assert.IsTrue(File.Exists(day.FileName));
        Console.Write($"Day {day.DayNumber,2}: {day.PuzzleName,-25} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed in constructor");

        // part 1
        s.Reset();
        s.Start();
        var part1Actual = day.Part1().ToString();
        s.Stop();
        Console.Write($"Part 1: {part1Actual,-25} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part1, part1Actual, $"Incorrect answer for Day {day.DayNumber} Part1");

        // part 2
        s.Reset();
        s.Start();
        var part2Actual = day.Part2().ToString();
        s.Stop();
        Console.Write($"Part 2: {part2Actual,-25} ");
        Console.WriteLine($"{s.ScaleMilliseconds()} ms elapsed");
        Assert.AreEqual(part2, part2Actual, $"Incorrect answer for Day {day.DayNumber} Part2");
    }
}
