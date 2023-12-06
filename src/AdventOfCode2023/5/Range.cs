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
}
