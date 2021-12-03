namespace aoc2021;

/// <summary>
/// Day 3: <see href="https://adventofcode.com/2021/day/3"/>
/// </summary>
public sealed class Day03 : Day
{
    private readonly List<string> _report;
    
    public Day03() : base(3, "Binary Diagnostic")
    {
        _report = Input.ToList();
    }

    public override string Part1()
    {
        var l = _report.Count / 2;
        var g = new StringBuilder();
        var e = new StringBuilder();
        
        foreach (var i in Enumerable.Range(0, _report[0].Length))
        {
            var ones = _report.Select(r => r[i]).Count(c => c == '1');
            g.Append(ones > l ? '1' : '0');
            e.Append(ones > l ? '0' : '1');
        }

        var gamma   = Convert.ToInt32(g.ToString(), 2);
        var epsilon = Convert.ToInt32(e.ToString(), 2);

        return $"{gamma * epsilon}";
    }

    public override string Part2()
    {
        var o = _report;
        var c = _report;
        
        var i = 0;
        while (o.Count > 1)
        {
            var most = o.Count(r => r[i] == '1') >= o.Count / 2.0 ? '1' : '0';
            o = o.Where(r => r[i] == most).ToList();
            i++;
        }
        var o2 = Convert.ToInt64(o.Single(), 2);

        i = 0;
        while (c.Count > 1)
        {
            var most = c.Count(r => r[i] == '1') >= c.Count / 2.0 ? '0' : '1';
            c = c.Where(r => r[i] == most).ToList();
            i++;
        }
        var co2 = Convert.ToInt64(c.Single(), 2);

        return $"{o2 * co2}";
    }
}
