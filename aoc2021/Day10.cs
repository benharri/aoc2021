namespace aoc2021;

/// <summary>
/// Day 10: <see href="https://adventofcode.com/2021/day/10"/>
/// </summary>
public sealed class Day10 : Day
{
    private static readonly Dictionary<char, char> MatchedBrackets = new()
    {
        {'(', ')'},
        {'[', ']'},
        {'{', '}'},
        {'<', '>'}
    };

    private static readonly Dictionary<char, long> Scores = new()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private static readonly Dictionary<char, long> ScoresPart2 = new()
    {
        { '(', 1 },
        { '[', 2 },
        { '{', 3 },
        { '<', 4 }
    };

    private readonly long _score1;
    private readonly List<long> _scores2 = new();

    public Day10() : base(10, "Syntax Scoring")
    {
        _score1 = 0L;
        foreach (var line in Input)
        {
            var corrupt = false;
            var s = new Stack<char>();

            foreach (var c in line)
            {
                if (ScoresPart2.ContainsKey(c))
                {
                    s.Push(c);
                }
                else
                {
                    if (c == MatchedBrackets[s.Pop()]) continue;
                    _score1 += Scores[c];
                    corrupt = true;
                    break;
                }
            }

            if (corrupt) continue;
            var score2 = 0L;
            while (s.Any())
            {
                score2 *= 5;
                score2 += ScoresPart2[s.Pop()];
            }

            _scores2.Add(score2);
        }
    }

    public override string Part1() => $"{_score1}";

    public override string Part2()
    {
        var sorted = _scores2.OrderBy(i => i).ToList();
        return $"{sorted[sorted.Count / 2]}";
    }
}
