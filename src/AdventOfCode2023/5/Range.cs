namespace AdventOfCode2023._5;

public sealed class Range
{
    public Range(long start, long length)
    {
        Start = start;
        Length = length;
    }

    public long Start { get; }

    public long Length { get; }

    public long End => Start + Length - 1;
}
