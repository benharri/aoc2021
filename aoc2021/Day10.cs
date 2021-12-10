using System.Collections;

namespace aoc2021;

/// <summary>
/// Day 10: <see href="https://adventofcode.com/2021/day/10"/>
/// </summary>
public sealed class Day10 : Day
{
    private readonly List<string> _input;
    private readonly char[] _openers = { '(', '[', '{', '<' };
    private readonly char[] _closers = { ')', ']', '}', '>' };

    private readonly Dictionary<char, int> _scores = new()
    {
        { ')', 3 },
        { ']', 57 },
        { '}', 1197 },
        { '>', 25137 }
    };

    private readonly Dictionary<char, int> _scoresClosing = new()
    {
        { '(', 1 },
        { '[', 2 },
        { '{', 3 },
        { '<', 4 }
    };
    
    public Day10() : base(10, "Syntax Scoring")
    {
        _input = Input.ToList();
    }

    public override string Part1()
    {
        var s = new Stack<char>();
        var score = 0;
        foreach (var line in _input)
        {
            foreach (var c in line)
            {
                if (_openers.Contains(c))
                {
                    s.Push(c);
                }
                else
                {
                    var y = s.Pop();
                    if (Math.Abs(y - c) <= 3) continue;
                    score += _scores[c];
                    break;
                }
            }
        }

        return score.ToString();
    }

    public override string Part2()
    {
    }
}
