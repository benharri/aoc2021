namespace aoc2021;

/// <summary>
/// Day 19: <see href="https://adventofcode.com/2021/day/19"/>
/// </summary>
public sealed class Day19 : Day
{
    private static readonly (int, int, int)[] Axes =
        { (0, 1, 0), (0, -1, 0), (1, 0, 0), (-1, 0, 0), (0, 0, 1), (0, 0, -1) };

    private readonly List<HashSet<Vector3>> _scans;
    private List<HashSet<Vector3>> _scanners = new();

    public Day19() : base(19, "Beacon Scanner")
    {
        _scans = Input
            .Aggregate(new List<HashSet<Vector3>>(), (list, line) =>
            {
                if (string.IsNullOrWhiteSpace(line)) return list;

                if (line.StartsWith("---"))
                {
                    list.Add(new());
                    return list;
                }

                var parts = line.Split(',').Select(int.Parse).ToList();
                list[^1].Add((parts[0], parts[1], parts[2]));
                return list;
            });
    }

    private static Vector3 Transform(Vector3 pt, Vector3 up, int rotation)
    {
        var reoriented = up switch
        {
            (0, 1, 0) => pt,
            (0, -1, 0) => (pt.X, -pt.Y, -pt.Z),
            (1, 0, 0) => (pt.Y, pt.X, -pt.Z),
            (-1, 0, 0) => (pt.Y, -pt.X, pt.Z),
            (0, 0, 1) => (pt.Y, pt.Z, pt.X),
            (0, 0, -1) => (pt.Y, -pt.Z, -pt.X),
            _ => throw new("Invalid up vector")
        };

        return rotation switch
        {
            0 => reoriented,
            1 => (reoriented.Z, reoriented.Y, -reoriented.X),
            2 => (-reoriented.X, reoriented.Y, -reoriented.Z),
            3 => (-reoriented.Z, reoriented.Y, reoriented.X),
            _ => throw new("Invalid rotation")
        };
    }

    private static Vector3 Translate(Vector3 p, Vector3 v) => (p.X + v.X, p.Y + v.Y, p.Z + v.Z);
    private static Vector3 Difference(Vector3 p1, Vector3 p2) => (p1.X - p2.X, p1.Y - p2.Y, p1.Z - p2.Z);
    
    private static int Manhattan(Vector3 a, Vector3 b)
    {
        var (dx, dy, dz) = Difference(a, b);
        return Math.Abs(dx) + Math.Abs(dy) + Math.Abs(dz);
    }
    
    private static (HashSet<Vector3> alignedBeacons, Vector3 translation, Vector3 up, int rotation)? Align(
        HashSet<Vector3> beacons1, IReadOnlyCollection<Vector3> beacons2)
    {
        // Fix beacons1, rotate beacons2
        foreach (var axis in Axes)
        {
            for (var rotation = 0; rotation < 4; rotation++)
            {
                var rotatedBeacons2 = new HashSet<Vector3>(beacons2.Select(b => Transform(b, axis, rotation)));

                foreach (var b1 in beacons1)
                {
                    foreach (var matchingB1InB2 in rotatedBeacons2)
                    {
                        var delta = Difference(b1, matchingB1InB2);
                        var transformedBeacons2 =
                            new HashSet<Vector3>(rotatedBeacons2.Select(b => Translate(b, delta)));

                        var intersection = new HashSet<Vector3>();
                        intersection.UnionWith(transformedBeacons2);
                        intersection.IntersectWith(beacons1);
                        if (intersection.Count >= 12)
                            return (transformedBeacons2, delta, axis, rotation);
                    }
                }
            }
        }

        return null;
    }


    private static (List<HashSet<Vector3>> scans, List<HashSet<Vector3>> scanners) Reduce(
        IReadOnlyList<HashSet<Vector3>> scans, IReadOnlyList<HashSet<Vector3>> scanners)
    {
        var toRemove = new HashSet<int>();
        for (var i = 0; i < scans.Count - 1; i++)
        {
            for (var j = i + 1; j < scans.Count; j++)
            {
                if (toRemove.Contains(j)) continue;

                var alignment = Align(scans[i], scans[j]);
                if (alignment == null) continue;

                // Convert all scanners from j coordinates to i coordinates
                foreach (var s in scanners[j])
                    scanners[i].Add(Translate(alignment.Value.translation,
                        Transform(s, alignment.Value.up, alignment.Value.rotation)));

                // Merge the scan sets
                scans[i].UnionWith(alignment.Value.alignedBeacons);
                toRemove.Add(j);
            }
        }

        // Skip all scans and scanners that were merged
        return (scans.Where((_, i) => !toRemove.Contains(i)).ToList(),
            scanners.Where((el, i) => !toRemove.Contains(i)).ToList());
    }


    public override object Part1()
    {
        var scans = _scans;
        _scanners = scans.Select(_ => new HashSet<Vector3> { (0, 0, 0) }).ToList();
        while (scans.Count > 1)
            (scans, _scanners) = Reduce(scans, _scanners);
        
        return scans[0].Count;
    }

    public override object Part2()
    {
        var scannerList = _scanners[0].ToList();
        return Enumerable.Range(0, scannerList.Count - 1)
            .SelectMany(i => Enumerable.Range(i + 1, scannerList.Count - i - 1).Select(j => (i, j)))
            .Max(pair => Manhattan(scannerList[pair.i], scannerList[pair.j]));
    }

    private record struct Vector3(int X, int Y, int Z)
    {
        public static implicit operator Vector3((int x, int y, int z) value) => new(value.x, value.y, value.z);
    }
}