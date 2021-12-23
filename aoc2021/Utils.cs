namespace aoc2021;

public class DefaultDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
{
    public DefaultDict()
    {
    }

    public DefaultDict(IDictionary<TKey, TValue> dict) : base(dict)
    {
    }

    public TValue? DefaultValue;

    public DefaultDict(IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
    {
    }

    public new TValue? this[TKey key]
    {
        get => TryGetValue(key, out var t) ? t : DefaultValue;
        set
        {
            if (value != null) base[key] = value;
        }
    }
}

public class Tree<T>
{
    public class Node
    {
        public Node? Parent { get; private set; }
        public T Data { get; set; }
        private List<Node?> Children { get; }

        public Node? Left
        {
            get => Children.Count >= 1 ? Children[0] : null;

            set
            {
                if (value != null) value.Parent = this;
                if (Children.Count >= 1) Children[0] = value;
                else Children.Add(value);
            }
        }

        public Node? Right
        {
            get => Children.Count >= 2 ? Children[1] : null;

            set
            {
                if (value != null) value.Parent = this;
                if (Children.Count >= 2) Children[1] = value;
                else if (Children.Count == 0) Children.Add(null);
                Children.Add(value);
            }
        }

        public Node(Node? parent, T data)
        {
            Parent = parent;
            Data = data;
            Children = new();
        }

        public int DistanceToParent(Node parent)
        {
            var current = this;
            var dist = 0;
            while (current != parent)
            {
                dist++;
                current = current?.Parent;
            }

            return dist;
        }
    }

    public Node Root { get; }
    public Tree(Node root) => Root = root;
}

public class Dijkstra<TCell, TMid> where TCell : notnull
{
    public Func<TCell, IEnumerable<TMid>>? Neighbors;
    public Func<TMid, int>? Distance;
    public Func<TCell, TMid, TCell>? Cell;

    public Dictionary<TCell, int> ComputeAll(TCell start, IEnumerable<TCell> all)
    {
        var dist = new Dictionary<TCell, int>();
        foreach (var cell in all)
        {
            dist[cell] = int.MaxValue;
        }

        dist[start] = 0;
        var queue = new PriorityQueue<TCell, int>(dist.Select(pair => (pair.Key, pair.Value)));
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            var current = dist[cell];
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                var weight = Distance!(neighbor);
                if (current + weight < dist[other])
                {
                    dist[other] = current + weight;
                }
            }
        }

        return dist;
    }

    public Dictionary<TCell, int> Compute(TCell start)
    {
        var dist = new DefaultDict<TCell, int> { DefaultValue = int.MaxValue, [start] = 0 };
        var seen = new HashSet<TCell>();
        var queue = new PriorityQueue<TCell, int>();
        queue.Enqueue(start, 0);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            seen.Add(cell);
            var current = dist[cell];
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                var weight = Distance!(neighbor);
                if (!seen.Contains(other)) queue.Enqueue(other, current + weight);
                if (current + weight < dist[other])
                {
                    dist[other] = current + weight;
                }
            }
        }

        return dist;
    }

    public int Count(TCell start, Func<TCell, bool> count)
    {
        var total = 0;
        var seen = new HashSet<TCell>();
        var queue = new Queue<TCell>();
        queue.Enqueue(start);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            seen.Add(cell);
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                if (count(other))
                {
                    total++;
                    continue;
                }

                if (!seen.Contains(other)) queue.Enqueue(other);
            }
        }

        return total;
    }

    public Dictionary<TCell, int> ComputeWhere(TCell start, Func<TCell, bool> valid)
    {
        var dist = new DefaultDict<TCell, int> { DefaultValue = int.MaxValue, [start] = 0 };
        var seen = new HashSet<TCell>();
        var queue = new PriorityQueue<TCell, int>();
        queue.Enqueue(start, 0);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            seen.Add(cell);
            var current = dist[cell];
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                if (!valid(other)) continue;
                var weight = Distance!(neighbor);
                if (!seen.Contains(other)) queue.Enqueue(other, current + weight);
                if (current + weight < dist[other])
                {
                    dist[other] = current + weight;
                }
            }
        }

        return dist;
    }

    public Dictionary<TCell, (int Dist, TCell From)> ComputeFrom(TCell start, Func<TCell, bool>? valid = null)
    {
        valid ??= _ => true;
        var dist = new DefaultDict<TCell, (int Dist, TCell From)>
            { DefaultValue = (int.MaxValue, default)!, [start] = (0, default)! };
        var seen = new HashSet<TCell>();
        var queue = new PriorityQueue<TCell, int>();
        queue.Enqueue(start, 0);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            seen.Add(cell);
            var current = dist[cell].Dist;
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                if (!valid(other)) continue;
                var weight = Distance!(neighbor);
                if (!seen.Contains(other)) queue.Enqueue(other, current + weight);
                if (current + weight < dist[other].Dist)
                {
                    dist[other] = (current + weight, cell);
                }
            }
        }

        return dist;
    }

    public Dictionary<TCell, (int Dist, TCell From)> ComputePath(TCell start, TCell target,
        Func<TCell, bool>? valid = null)
    {
        valid ??= _ => true;
        var dist = new DefaultDict<TCell, (int Dist, TCell From)>
            { DefaultValue = (int.MaxValue, default)!, [start] = (0, default)! };
        var seen = new HashSet<TCell>();
        var queue = new PriorityQueue<TCell, int>();
        queue.Enqueue(start, 0);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            seen.Add(cell);
            if (Equals(cell, target)) return dist;
            var current = dist[cell].Dist;
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                if (!valid(other)) continue;
                var weight = Distance!(neighbor);
                if (!seen.Contains(other)) queue.Enqueue(other, current + weight);
                if (current + weight < dist[other].Dist)
                {
                    dist[other] = (current + weight, cell);
                }
            }
        }

        return dist;
    }

    public int ComputeFind(TCell start, TCell target, Func<TCell, bool>? valid = null)
    {
        valid ??= _ => true;
        var dist = new DefaultDict<TCell, int> { DefaultValue = int.MaxValue, [start] = 0 };
        var seen = new HashSet<TCell>();
        var queue = new PriorityQueue<TCell, int>();
        queue.Enqueue(start, 0);
        while (queue.Count > 0)
        {
            var cell = queue.Dequeue();
            if (seen.Contains(cell)) continue;
            var current = dist[cell];
            if (Equals(cell, target)) return current;
            seen.Add(cell);
            foreach (var neighbor in Neighbors!(cell))
            {
                var other = Cell!(cell, neighbor);
                if (!valid(other)) continue;
                var weight = Distance!(neighbor);
                if (!seen.Contains(other)) queue.Enqueue(other, current + weight);
                if (current + weight < dist[other])
                {
                    dist[other] = current + weight;
                }
            }
        }

        return -1;
    }
}

public interface IDijkstra<TCell, TMid>
{
    IEnumerable<TMid> GetNeighbors(TCell cell);

    int GetWeight(TMid mid);

    TCell GetNeighbor(TCell from, TMid mid);
}

public static class DijkstraExtensions
{
    private static Dijkstra<TCell, TMid> Build<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra) where TCell : notnull
    {
        return new()
        {
            Neighbors = dijkstra.GetNeighbors,
            Distance = dijkstra.GetWeight,
            Cell = dijkstra.GetNeighbor
        };
    }

    public static Dijkstra<TCell, TMid> ToDijkstra<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra)
        where TCell : notnull
    {
        return dijkstra.Build();
    }

    public static Dictionary<TCell, int> DijkstraAll<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra, TCell start,
        IEnumerable<TCell> all) where TCell : notnull
    {
        return dijkstra.Build().ComputeAll(start, all);
    }

    public static Dictionary<TCell, int> Dijkstra<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra, TCell start)
        where TCell : notnull
    {
        return dijkstra.Build().Compute(start);
    }

    public static Dictionary<TCell, int> DijkstraWhere<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra, TCell start,
        Func<TCell, bool> valid) where TCell : notnull
    {
        return dijkstra.Build().ComputeWhere(start, valid);
    }

    public static Dictionary<TCell, (int Dist, TCell From)> DijkstraFrom<TCell, TMid>(
        this IDijkstra<TCell, TMid> dijkstra, TCell start, Func<TCell, bool>? valid = null) where TCell : notnull
    {
        return dijkstra.Build().ComputeFrom(start, valid);
    }

    public static int DijkstraFind<TCell, TMid>(this IDijkstra<TCell, TMid> dijkstra, TCell start, TCell target,
        Func<TCell, bool>? valid = null) where TCell : notnull
    {
        return dijkstra.Build().ComputeFind(start, target, valid);
    }

    public static Dictionary<TCell, (int Dist, TCell From)> DijkstraPath<TCell, TMid>(
        this IDijkstra<TCell, TMid> dijkstra, TCell start, TCell target, Func<TCell, bool>? valid = null)
        where TCell : notnull
    {
        return dijkstra.Build().ComputePath(start, target, valid);
    }

    public static IReadOnlyCollection<TCell> GetPathTo<TCell>(this Dictionary<TCell, (int Dist, TCell From)> dist,
        TCell target) where TCell : notnull
    {
        var list = new LinkedList<TCell>();
        list.AddFirst(target);
        while (true)
        {
            if (!dist.TryGetValue(target, out var pair)) return Array.Empty<TCell>();
            if (pair.Dist == 0) break;
            list.AddFirst(target = pair.From);
        }

        return list;
    }
}