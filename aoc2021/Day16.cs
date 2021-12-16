namespace aoc2021;

/// <summary>
/// Day 16: <see href="https://adventofcode.com/2021/day/16"/>
/// </summary>
public sealed class Day16 : Day
{
    private readonly Packet _packet;

    public Day16() : base(16, "Packet Decoder")
    {
        var bits = string.Join(string.Empty,
            Input.First().Select(c => Convert.ToString(Convert.ToInt32(c.ToString(), 16), 2).PadLeft(4, '0')));
        (_packet, _) = Packet.FromBinaryString(bits);
    }

    private record Packet(int Version, int TypeId, long Value, List<Packet> Packets)
    {
        public static (Packet packet, int offset) FromBinaryString(string input)
        {
            var index = 0;
            var subPackets = new List<Packet>();
            var version = Convert.ToInt32(input[..(index += 3)], 2);
            var typeId = Convert.ToInt32(input[index..(index += 3)], 2);

            if (typeId == 4)
            {
                // value packet, gather value bits and return right away
                var literalBits = new StringBuilder();
                foreach (var chunk in input.Skip(index).Chunk(5))
                {
                    literalBits.Append(chunk[1..5]);
                    index += 5;
                    if (chunk[0] == '0') break;
                }

                return (new(version, typeId, Convert.ToInt64(literalBits.ToString(), 2), new()), index);
            }

            switch (input[index++])
            {
                case '0':
                    var subPacketLength = Convert.ToInt32(input[index..(index += 15)], 2);
                    while (subPacketLength > 0)
                    {
                        var (packet, offset) = FromBinaryString(input[index..(index + subPacketLength)]);
                        subPackets.Add(packet);
                        subPacketLength -= offset;
                        index += offset;
                    }

                    break;
                case '1':
                    foreach (var _ in Enumerable.Range(0, Convert.ToInt32(input[index..(index += 11)], 2)))
                    {
                        var (packet, offset) = FromBinaryString(input[index..]);
                        subPackets.Add(packet);
                        index += offset;
                    }

                    break;
            }

            return (new(version, typeId, 0, subPackets), index);
        }

        public long VersionTotal => Version + Packets.Sum(p => p.VersionTotal);

        public long Eval =>
            TypeId switch
            {
                0 => Packets.Sum(p => p.Eval),
                1 => Packets.Aggregate(1L, (p, i) => p * i.Eval),
                2 => Packets.Min(p => p.Eval),
                3 => Packets.Max(p => p.Eval),
                4 => Value,
                5 => Packets[0].Eval > Packets[1].Eval ? 1 : 0,
                6 => Packets[0].Eval < Packets[1].Eval ? 1 : 0,
                7 => Packets[0].Eval == Packets[1].Eval ? 1 : 0,
                _ => throw new ArgumentException("invalid packet type", nameof(TypeId))
            };
    }

    public override object Part1() => _packet.VersionTotal;

    public override object Part2() => _packet.Eval;
}