namespace aoc2021;

public record Node
{
    public int X { get; init; }
    public int Y { get; init; }
    public int Risk { get; init; }
    public int Distance { get; set; } = int.MaxValue;
    public bool Visited { get; set; }
}

/// <summary>
/// Day 15: <see href="https://adventofcode.com/2021/day/15"/>
/// </summary>
public sealed class Day15 : Day
{
    private static readonly (int x, int y)[] Adjacent = { (-1, 0), (1, 0), (0, -1), (0, 1) };
    private readonly Dictionary<(int x, int y), Node> _fullGrid;
    private readonly Dictionary<(int x, int y), Node> _grid;
    private readonly int _width;

    public Day15() : base(15, "Chiton")
    {
        _grid = Input
            .SelectMany((line, y) =>
                line.Select((c, x) => (coord: (x, y), node: new Node { X = x, Y = y, Risk = c - '0' })))
            .ToDictionary(t => t.coord, t => t.node);

        _width = (int)Math.Sqrt(_grid.Count);

        _fullGrid =
            Enumerable.Range(0, 5).SelectMany(i =>
                    Enumerable.Range(0, 5).SelectMany(j =>
                        _grid.Select(kvp =>
                        {
                            var ((x, y), node) = kvp;
                            var newKey = (x: x + _width * i, y: y + _width * j);
                            return (newKey,
                                node: new Node { X = newKey.x, Y = newKey.y, Risk = (node.Risk + i + j - 1) % 9 + 1 });
                        })))
                .ToDictionary(t => t.newKey, t => t.node);
    }

    private static IEnumerable<Node> GetNeighborsAt(IReadOnlyDictionary<(int x, int y), Node> grid, Node node)
    {
        foreach (var (i, j) in Adjacent)
        {
            var key = (node.X + i, node.Y + j);
            if (grid.ContainsKey(key) && !grid[key].Visited)
                yield return grid[key];
        }
    }

    private static int DijkstraCost(IReadOnlyDictionary<(int x, int y), Node> grid, Node target)
    {
        var searchQueue = new PriorityQueue<Node, int>();
        grid[(0, 0)].Distance = 0;
        searchQueue.Enqueue(grid[(0, 0)], 0);

        while (searchQueue.Count > 0)
        {
            var current = searchQueue.Dequeue();
            if (current.Visited) continue;
            current.Visited = true;
            if (current == target) return target.Distance;

            foreach (var neighbor in GetNeighborsAt(grid, current))
            {
                var alt = current.Distance + neighbor.Risk;
                if (alt < neighbor.Distance) neighbor.Distance = alt;
                if (neighbor.Distance != int.MaxValue) searchQueue.Enqueue(neighbor, neighbor.Distance);
            }
        }

        return target.Distance;
    }

    public override object Part1() =>
        DijkstraCost(_grid, _grid[(_width - 1, _width - 1)]);

    public override object Part2() =>
        DijkstraCost(_fullGrid, _fullGrid[(5 * _width - 1, 5 * _width - 1)]);
}