namespace aoc2021;

/// <summary>
/// Day 14: <see href="https://adventofcode.com/2021/day/14"/>
/// </summary>
public sealed class Day14 : Day
{
    private readonly string _template;
    private readonly Dictionary<string, string> _substitutionPairs;
    
    public Day14() : base(14, "Extended Polymerization")
    {
        _template = Input.First();
        _substitutionPairs = Input.Skip(2).Select(l => l.Split(" -> ")).ToDictionary(k => k[0], v => v[1]);
    }

    private string DoStep(string input)
    {
        var result = new StringBuilder();
        
        for (var i = 0; i < input.Length - 1; i++)
        {
            var k = input.Substring(i, 2);
            if (_substitutionPairs.ContainsKey(k))
            {
                result.Append(k[0]);
                result.Append(_substitutionPairs[k]);
            }
            else
            {
                result.Append(k);
            }
        }

        result.Append(input[^1]);

        return result.ToString();
    }

    public override object Part1()
    {
        var s = Enumerable.Range(0, 10).Aggregate(_template, (current, _) => DoStep(current));

        var most = s.ToCharArray()
            .GroupBy(c => c)
            .OrderByDescending(g => g.Count())
            .Select(g => g.Count())
            .ToList();
        
        return most.First() - most.Last();
    }

    public override object Part2()
    {
        return "";
    }
}
