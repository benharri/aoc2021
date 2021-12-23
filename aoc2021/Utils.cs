namespace aoc2021;

public class DefaultDict<TKey, TValue> : Dictionary<TKey, TValue> where TKey : notnull
{
    public TValue? DefaultValue;

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