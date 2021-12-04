namespace aoc2021;

/// <summary>
/// Day 4: <see href="https://adventofcode.com/2021/day/4"/>
/// </summary>
public sealed class Day04 : Day
{
    private readonly List<int> _call;
    private readonly List<List<int>> _boards;
    
    public Day04() : base(4, "Giant Squid")
    {
        _call = new(Input.First().Split(',').Select(int.Parse).ToList());
        _boards = new();

        List<int> currentBoard = new();
        foreach (var line in Input.Skip(2))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                _boards.Add(currentBoard);
                currentBoard = new();
                continue;
            }

            currentBoard.AddRange(line
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse));
        }

        if (currentBoard.Any()) _boards.Add(currentBoard);
    }

    public override string Part1()
    {
        int i = 1, b = FirstWin(i);
        while (b == -1)
        {
            i++;
            b = FirstWin(i);
        }

        var called = _call.Take(i).ToHashSet();
        return $"{called.Last() * _boards[b].Where(x => !called.Contains(x)).Sum()}";
    }

    private int FirstWin(int i)
    {
        var c = _call.Take(i).ToHashSet();
        for (var j = 0; j < _boards.Count; j++)
            if (HasWin(c, _boards[j])) return j;
        return -1;
    }

    private static bool HasWin(IReadOnlySet<int> c, IReadOnlyList<int> b) =>
        c.Contains(b[0])  && c.Contains(b[1])  && c.Contains(b[2])  && c.Contains(b[3])  && c.Contains(b[4])  ||
        c.Contains(b[5])  && c.Contains(b[6])  && c.Contains(b[7])  && c.Contains(b[8])  && c.Contains(b[9])  ||
        c.Contains(b[10]) && c.Contains(b[11]) && c.Contains(b[12]) && c.Contains(b[13]) && c.Contains(b[14]) ||
        c.Contains(b[15]) && c.Contains(b[16]) && c.Contains(b[17]) && c.Contains(b[18]) && c.Contains(b[19]) ||
        c.Contains(b[20]) && c.Contains(b[21]) && c.Contains(b[22]) && c.Contains(b[23]) && c.Contains(b[24]) ||
        c.Contains(b[0])  && c.Contains(b[5])  && c.Contains(b[10]) && c.Contains(b[15]) && c.Contains(b[20]) ||
        c.Contains(b[1])  && c.Contains(b[6])  && c.Contains(b[11]) && c.Contains(b[16]) && c.Contains(b[21]) ||
        c.Contains(b[2])  && c.Contains(b[7])  && c.Contains(b[12]) && c.Contains(b[17]) && c.Contains(b[22]) ||
        c.Contains(b[3])  && c.Contains(b[8])  && c.Contains(b[13]) && c.Contains(b[18]) && c.Contains(b[23]) ||
        c.Contains(b[4])  && c.Contains(b[9])  && c.Contains(b[14]) && c.Contains(b[19]) && c.Contains(b[24]);

    public override string Part2()
    {
        Dictionary<int, bool> wonBoards = new();
        for (var i = 0; i < _boards.Count; i++)
            wonBoards[i] = false;

        var j = 0;
        while (wonBoards.Values.Count(b => b) != wonBoards.Count - 1)
        {
            var c = _call.Take(j).ToHashSet();
            for (var u = 0; u < _boards.Count; u++)
                wonBoards[u] = HasWin(c, _boards[u]);
            j++;
        }

        var called = _call.Take(j).ToHashSet();
        var b = wonBoards.Single(kvp => !kvp.Value).Key;
        return $"{called.Last() * _boards[b].Where(x => !called.Contains(x)).Sum()}";
    }
}
