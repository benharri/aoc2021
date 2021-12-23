namespace aoc2021;

/// <summary>
/// Day 23: <see href="https://adventofcode.com/2021/day/23"/>
/// </summary>
public sealed class Day23 : Day
{
    private readonly List<char> _crabs;

    public Day23() : base(23, "Amphipod")
    {
        _crabs = Input.SelectMany(l => l).Where(char.IsLetter).ToList();
    }

    private static IEnumerable<int> BreadthFirstSearch(string s, int i)
    {
        var visited = new HashSet<int>();
        var queue = new Queue<int>();
        queue.Enqueue(i);
        visited.Add(i);
        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            var near = new[] { next - 1, next + 1 }.Where(p => !visited.Contains(p) && p >= 0 && p < s.Length);
            foreach (var n in near)
            {
                if (!char.IsWhiteSpace(s[n])) continue;
                yield return n;
                queue.Enqueue(n);
                visited.Add(n);
            }
        }
    }

    private static Dijkstra<State, (State state, int distance)> GetPathFinder(int size)
    {
        return new()
        {
            Neighbors = state =>
            {
                // Find all neighbors from the current state
                var possible = new List<(State, int)>();
                var entries = new[] { 2, 4, 6, 8 };
                // Add each way of taking an item out of a hole into the hallway
                foreach (var i in entries)
                {
                    var hole = state[i / 2 - 1];
                    if (string.IsNullOrWhiteSpace(hole)) continue;
                    var targets = BreadthFirstSearch(state.Hallway, i).Except(entries).ToList();
                    foreach (var target in targets)
                    {
                        var data = state.Hallway.ToCharArray();
                        data[target] = hole.Trim()[0];
                        var newHole = hole.Trim()[1..].PadLeft(size);
                        var next = State.New(state, data, i / 2 - 1, newHole);
                        var cost = Math.Abs(target - i) + (size - newHole.Trim().Length);
                        cost *= 10.Pow(data[target] - 'A');
                        possible.Add((next, cost));
                    }
                }

                foreach (var (at, which) in state.Hallway.Indexed().WhereValue(char.IsLetter))
                {
                    var dest = which - 'A';
                    if (!BreadthFirstSearch(state.Hallway, at).Intersect(entries).Select(i => i / 2 - 1)
                            .Contains(dest)) continue;
                    if (state[dest]!.Trim().Any(c => c != which)) continue;
                    var data = state.Hallway.ToCharArray();
                    data[at] = ' ';
                    var next = State.New(state, data, dest, (which + state[dest]!.Trim()).PadLeft(size));
                    var cost = Math.Abs(at - (dest + 1) * 2) + (size - state[dest]!.Trim().Length);
                    cost *= 10.Pow(dest);
                    possible.Add((next, cost));
                }

                return possible;
            },
            Distance = tuple => tuple.distance,
            Cell = (_, tuple) => tuple.state
        };
    }

    public override object Part1()
    {
        var start = new State("           ",
            $"{_crabs[0]}{_crabs[4]}",
            $"{_crabs[1]}{_crabs[5]}",
            $"{_crabs[2]}{_crabs[6]}",
            $"{_crabs[3]}{_crabs[7]}");
        var dest = new State("           ", "AA", "BB", "CC", "DD");

        return GetPathFinder(2).ComputeFind(start, dest);
    }

    public override object Part2()
    {
        var start = new State("           ",
            $"{_crabs[0]}DD{_crabs[4]}",
            $"{_crabs[1]}CB{_crabs[5]}",
            $"{_crabs[2]}BA{_crabs[6]}",
            $"{_crabs[3]}AC{_crabs[7]}");
        var dest = new State("           ", "AAAA", "BBBB", "CCCC", "DDDD");

        return GetPathFinder(4).ComputeFind(start, dest);
    }

    private record State(string Hallway, string A, string B, string C, string D)
    {
        public string? this[int i] =>
            i switch
            {
                0 => A,
                1 => B,
                2 => C,
                3 => D,
                _ => null
            };

        public static State New(State from, char[] hall, int i, string hole) =>
            new(new(hall),
                i == 0 ? hole : from.A,
                i == 1 ? hole : from.B,
                i == 2 ? hole : from.C,
                i == 3 ? hole : from.D);
    }
}