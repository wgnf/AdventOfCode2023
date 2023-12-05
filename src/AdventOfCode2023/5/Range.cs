using System.Text;

namespace AdventOfCode2023._5;

internal sealed class Range
{
    public Range(long start, long length)
    {
        Start = start;
        Length = length;
    }

    public long Start { get; }

    public long Length { get; }

    public IEnumerable<long> GetValues()
    {
        for (var value = Start; value < Start + Length; value++)
        {
            yield return value;
        }
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        builder.Append($"S = {Start}, L = {Length}");
        return builder.ToString();
    }
}
