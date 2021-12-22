namespace aoc2021;

/// <summary>
/// Day 22: <see href="https://adventofcode.com/2021/day/22"/>
/// </summary>
public sealed class Day22 : Day
{
    private readonly List<Instruction> _instructions = new();
    
    public Day22() : base(22, "Reactor Reboot")
    {
        foreach (var line in Input)
        {
            var s = line.Split(' ');
            var e = s[1]
                .Split(',')
                .Select(i => i.Split('=')[1])
                .SelectMany(l => l.Split(".."))
                .Select(int.Parse)
                .ToList();
            
            _instructions.Add(new(s[0] == "off", new(new(e[0], e[1]), new(e[2], e[3]), new(e[4], e[5]))));
        }
    }

    private long ActiveCubes(int x, Region3D region)
    {
        if (region.IsEmpty || x < 0) return 0;
        
        var intersection = region.Intersect(_instructions[x].Region);
        var activeInRegion = ActiveCubes(x - 1, region);
        var activeInIntersection = ActiveCubes(x - 1, intersection);
        var activeOutsideIntersection = activeInRegion - activeInIntersection;

        // outside the intersection is unaffected, the rest is either on or off:
        return _instructions[x].Disable ? activeOutsideIntersection : activeOutsideIntersection + intersection.Volume;
    
    }

    public override object Part1()
    {
        var f = new RangeSegment(-50, 50);
        return ActiveCubes(_instructions.Count - 1, new(f, f, f));
    }

    public override object Part2()
    {
        var f = new RangeSegment(-int.MaxValue, int.MaxValue);
        return ActiveCubes(_instructions.Count - 1, new(f, f, f));
    }

    private record Instruction(bool Disable, Region3D Region);

    private record RangeSegment(int From, int To)
    {
        public bool IsEmpty => From > To;
        public long Length => IsEmpty ? 0 : To - From + 1;

        public RangeSegment Intersect(RangeSegment other) =>
            new(Math.Max(From, other.From), Math.Min(To, other.To));
    }

    private record Region3D(RangeSegment X, RangeSegment Y, RangeSegment Z)
    {
        public bool IsEmpty => X.IsEmpty || Y.IsEmpty || Z.IsEmpty;
        public long Volume => X.Length * Y.Length * Z.Length;

        public Region3D Intersect(Region3D other) =>
            new(X.Intersect(other.X), Y.Intersect(other.Y), Z.Intersect(other.Z));
    }
}
