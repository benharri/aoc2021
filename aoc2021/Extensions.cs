﻿namespace aoc2021;

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
}