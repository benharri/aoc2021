namespace aoc2021;

/// <summary>
/// Day 21: <see href="https://adventofcode.com/2021/day/21"/>
/// </summary>
public sealed class Day21 : Day
{
    private readonly int _player1, _player2;
    private readonly Dictionary<int, ulong> _possibleRollOutComes = new();
    private int _deterministicDie = 1;
    private int _rollCount;
    private ulong _player1Victories, _player2Victories;

    public Day21() : base(21, "Dirac Dice")
    {
        var s = Input
            .Select(l => l.Split(": ")[1])
            .Select(int.Parse)
            .ToList();

        _player1 = s[0];
        _player2 = s[1];
    }

    private int Turn(int pos)
    {
        var moves = _deterministicDie;
        IncMod(ref _deterministicDie);
        moves += _deterministicDie;
        IncMod(ref _deterministicDie);
        moves += _deterministicDie;
        IncMod(ref _deterministicDie);
        return (moves + pos - 1) % 10 + 1;
    }

    private void IncMod(ref int i, int limit = 100)
    {
        _rollCount++;
        if (i >= limit) i = 1;
        else i++;
    }

    private void RollDiracDie(int player1Points, int player2Points, int player1Pos, int player2Pos,
        int playerTurn, ulong universes)
    {
        if (player1Points > 21 || player2Points > 21) return;

        if (playerTurn == 1)
            foreach (var (key, value) in _possibleRollOutComes)
            {
                var pts = MoveSpaces(key, player1Pos);
                if (player1Points + pts < 21)
                    RollDiracDie(player1Points + pts, player2Points, pts, player2Pos, 2, value * universes);
                else
                    _player1Victories += universes * value;
            }
        else
            foreach (var (key, value) in _possibleRollOutComes)
            {
                var pts = MoveSpaces(key, player2Pos);
                if (player2Points + pts < 21)
                    RollDiracDie(player1Points, player2Points + pts, player1Pos, pts, 1, value * universes);
                else
                    _player2Victories += universes * value;
            }
    }

    private static int MoveSpaces(int numSpaces, int currentSpace)
    {
        int spaceLandOn, toAdd;

        if (numSpaces > 10)
            toAdd = numSpaces % 10;
        else
            toAdd = numSpaces;

        if (currentSpace + toAdd > 10)
            spaceLandOn = (currentSpace + toAdd) % 10;
        else
            spaceLandOn = currentSpace + toAdd;

        return spaceLandOn;
    }

    public override object Part1()
    {
        int p1Score = 0, p2Score = 0, p1Pos = _player1, p2Pos = _player2;

        while (true)
        {
            if (p2Score >= 1000) break;

            var dest1 = Turn(p1Pos);
            p1Score += dest1;
            p1Pos = dest1;

            if (p1Score >= 1000) break;

            var dest2 = Turn(p2Pos);
            p2Score += dest2;
            p2Pos = dest2;
        }

        return Math.Min(p1Score, p2Score) * _rollCount;
    }

    public override object Part2()
    {
        for (var x = 1; x <= 3; x++)
        for (var y = 1; y <= 3; y++)
        for (var z = 1; z <= 3; z++)
            _possibleRollOutComes[x + y + z] =
                _possibleRollOutComes.GetValueOrDefault(x + y + z, 0ul) + 1ul;

        RollDiracDie(0, 0, _player1, _player2, 1, 1);

        return Math.Max(_player1Victories, _player2Victories);
    }
}