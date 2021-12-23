using System.Numerics;

namespace aoc2021;

public static class Extensions
{
    /// <summary>
    ///     increased accuracy for stopwatch based on frequency.
    ///     <see
    ///         href="http://geekswithblogs.net/BlackRabbitCoder/archive/2012/01/12/c.net-little-pitfalls-stopwatch-ticks-are-not-timespan-ticks.aspx">
    ///         blog
    ///         details here
    ///     </see>
    /// </summary>
    /// <param name="stopwatch"></param>
    /// <returns></returns>
    public static double ScaleMilliseconds(this Stopwatch stopwatch) =>
        1_000 * stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;

    /// <summary>
    /// Does a <see cref="Range"/> include a given int?
    /// </summary>
    /// <param name="range"></param>
    /// <param name="i"></param>
    /// <returns></returns>
    public static bool Contains(this Range range, int i) =>
        i >= range.Start.Value && i <= range.End.Value;
    
    /// <summary>
    /// Creates a new BigInteger from a binary (Base2) string
    /// <see href="https://gist.github.com/mjs3339/73042bc0e717f98796ee9fa131e458d4" />
    /// </summary>
    public static BigInteger BigIntegerFromBinaryString(this string binaryValue)
    {
        BigInteger res = 0;
        if (binaryValue.Count(b => b == '1') + binaryValue.Count(b => b == '0') != binaryValue.Length) return res;
        foreach (var c in binaryValue)
        {
            res <<= 1;
            res += c == '1' ? 1 : 0;
        }

        return res;
    }
    
    public static IEnumerable<IEnumerable<T>> Permute<T>(this IEnumerable<T> list)
    {
        var array = list as T[] ?? list.ToArray();
        return array.Length == 1
            ? new[] { array }
            : array.SelectMany(t => Permute(array.Where(x => !x!.Equals(t))), (v, p) => p.Prepend(v));
    }
    
    public static int Pow(this int i, int power)
    {
        var pow = (uint) power;
        var ret = 1;
        while (pow != 0)
        {
            if ((pow & 1) == 1) ret *= i;
            i *= i;
            pow >>= 1;
        }
        return ret;
    }
    
    public static IEnumerable<KeyValuePair<int, T>> Indexed<T>(this IEnumerable<T> source)
    {
        return source.Select((t, i) => new KeyValuePair<int, T>(i, t));
    }
    
    public static IEnumerable<KeyValuePair<TKey, TValue>> WhereValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> source, Func<TValue, bool> func)
    {
        return source.Where(pair => func(pair.Value));
    }
}
