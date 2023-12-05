namespace AdventOfCode2023.Utils;

internal static class EnumerableUtils
{
    public static IEnumerable<long> LongRange(long start, long length)
    {
        for (var value = start; value < start + length; value++)
        {
            yield return value;
        }
    }
}
