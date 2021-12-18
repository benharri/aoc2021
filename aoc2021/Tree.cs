namespace aoc2021;

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