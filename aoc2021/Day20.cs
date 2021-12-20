namespace aoc2021;

/// <summary>
/// Day 20: <see href="https://adventofcode.com/2021/day/20"/>
/// </summary>
public sealed class Day20 : Day
{
    private readonly ImmutableArray<bool> _enhancementAlgorithm;
    private readonly Image _initialImage;

    public Day20() : base(20, "Trench Map")
    {
        _enhancementAlgorithm = Input.First().Select(ch => ch == '#').ToImmutableArray();
        _initialImage = Parse(Input.Skip(2).ToList());
    }

    private Image Enhance(Image image, int count) =>
        Enumerable.Range(0, count).Aggregate(image, (current, _) => EnhanceOnce(current));

    private Image EnhanceOnce(Image image)
    {
        var bounds = image.Bounds.Grow();

        var result = bounds.Points
            .Where(pt => _enhancementAlgorithm[image.GetEnhanceInput(pt)])
            .ToImmutableHashSet();

        return new(bounds, result, image.InfiniteValue
            ? _enhancementAlgorithm.Last()
            : _enhancementAlgorithm.First());
    }

    private static Image Parse(IEnumerable<string> grid)
    {
        var image = ImmutableHashSet.CreateBuilder<Point>();
        foreach (var (line, y) in grid.Select((a, i) => (a, i)))
        {
            image.UnionWith(line.Select((ch, i) => (x: i, isLit: ch == '#'))
                .Where(p => p.isLit)
                .Select(p => new Point(p.x, y)));
        }

        var bounds = new Rect(image.Min(p => p.X), image.Min(p => p.Y), image.Max(p => p.X), image.Max(p => p.Y));
        return new(bounds, image.ToImmutable());
    }

    public override object Part1() =>
        Enhance(_initialImage, 2).PixelCount;

    public override object Part2() =>
        Enhance(_initialImage, 50).PixelCount;

    private record struct Point(int X, int Y);

    private record Rect(int MinX, int MinY, int MaxX, int MaxY)
    {
        public IEnumerable<Point> Points =>
            Enumerable.Range(MinY, MaxY - MinY + 1)
                .SelectMany(_ => Enumerable.Range(MinX, MaxX - MinX + 1), (y, x) => new Point(x, y));

        public bool Contains(Point pt) => pt.X >= MinX && pt.X <= MaxX && pt.Y >= MinY && pt.Y <= MaxY;
        public Rect Grow() => new(MinX - 1, MinY - 1, MaxX + 1, MaxY + 1);
    }

    private record Image(Rect Bounds, ImmutableHashSet<Point> Pixels, bool InfiniteValue = false)
    {
        private bool this[Point pt] =>
            Bounds.Contains(pt) ? Pixels.Contains(pt) : InfiniteValue;
        public int PixelCount => Pixels.Count(Bounds.Contains);

        public int GetEnhanceInput(Point pt)
        {
            var values =
                Enumerable.Range(pt.Y - 1, 3)
                    .SelectMany(_ => Enumerable.Range(pt.X - 1, 3), (yi, xi) => this[new(xi, yi)] ? 1 : 0);

            return values.Aggregate(0, (p, n) => (p << 1) | n);
        }
    }
}