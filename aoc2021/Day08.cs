namespace aoc2021;

/// <summary>
/// Day 8: <see href="https://adventofcode.com/2021/day/8"/>
/// </summary>
public sealed class Day08 : Day
{
    private static readonly List<char[]> PossibleMappings = "abcdefg".ToCharArray().Permute().Select(m => m.ToArray()).ToList();
    public Day08() : base(8, "Seven Segment Search")
    {
        UseTestInput = true;
    }

    private static int MatchDigit(char[] lit)
    {
        return new string(lit) switch
        {
            "1110111" => 0,
            "0010010" => 1,
            "1011101" => 2,
            "1011011" => 3,
            "0111010" => 4,
            "1101011" => 5,
            "1101111" => 6,
            "1010010" => 7,
            "1111111" => 8,
            "1111011" => 9,
            _ => -1
        };
    }

    private static int Decode(string line)
    {
        var s = line.Split(" | ").Select(s => s.Split(' ')).ToList();
        int i;
        for (i = 0; i < PossibleMappings.Count; i++)
        {
            var lit = "0000000".ToCharArray();
            var matches = new int[10];
            var failed = false;
            foreach (var signal in s[0])
            {
                foreach (var c in signal)
                    lit[Array.IndexOf(PossibleMappings[i], c)] = '1';
                
                var match = MatchDigit(lit);
                if (match == -1)
                {
                    failed = true;
                    break;
                }
                matches[match] = 1;
            }

            if (failed) continue;
            if (matches.All(m => m == 1)) break;
        }

        var mapping = PossibleMappings[i];
        return 1;
    }

    public override string Part1() =>
        Input
            .Select(line => line.Split(" | ")[1].Split(' '))
            .Select(outputs => outputs.Count(o => new[] { 2, 3, 4, 7 }.Contains(o.Length)))
            .Sum()
            .ToString();

    public override string Part2() => Input.Select(Decode).Sum().ToString();
}
