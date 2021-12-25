namespace aoc2021;

/// <summary>
/// Day 25: <see href="https://adventofcode.com/2021/day/25"/>
/// </summary>
public sealed class Day25 : Day
{
    private readonly char[][] _cucumbers;

    public Day25() : base(25, "Sea Cucumber")
    {
        _cucumbers = Input.Select(l => l.ToCharArray()).ToArray();
    }

    private static char[][]? DoStep(IReadOnlyList<char[]> arr)
    {
        var moved = false;
        var h = arr.Count;
        var w = arr[0].Length;
        var result = new char[h][];
        for (var i = 0; i < h; i++)
        {
            result[i] = new char[w];
            for (var j = 0; j < w; j++)
            {
                if (result[i][j] == 0) result[i][j] = arr[i][j];
                if (arr[i][j] != '>') continue;
                if (arr[i][(j + 1) % w] != '.') continue;
                result[i][j] = '.';
                result[i][(j + 1) % w] = '>';
                moved = true;
            }
        }

        var result2 = new char[h][];

        for (var i = 0; i < h; i++)
            result2[i] = new char[w];

        for (var i = 0; i < h; i++)
        for (var j = 0; j < w; j++)
        {
            if (result2[i][j] == 0) result2[i][j] = result[i][j];
            if (result[i][j] != 'v') continue;
            if (result[(i + 1) % h][j] != '.') continue;
            result2[i][j] = '.';
            result2[(i + 1) % h][j] = 'v';
            moved = true;
        }

        return moved ? result2 : null;
    }

    public override object Part1()
    {
        var arr = _cucumbers;
        var count = 0;
        while (arr != null)
        {
            // PrintCucumbers(arr);
            arr = DoStep(arr);
            count++;
        }

        return count;
    }

    private static void PrintCucumbers(IEnumerable<char[]> arr)
    {
        foreach (var r in arr)
        {
            Console.WriteLine();
            foreach (var c in r) Console.Write(c);
        }

        Console.WriteLine();
        Console.WriteLine("---");
    }

    public override object Part2() => "";
}