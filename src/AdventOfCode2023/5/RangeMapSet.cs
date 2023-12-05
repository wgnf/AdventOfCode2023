namespace AdventOfCode2023._5;

internal sealed class RangeMapSet
{
    public List<RangeMap> Items { get; } = [];

    public long GetMappedValue(long value)
    {
        var range = Items.Find(r => r.SourceRange.Start <= value && r.SourceRange.Start + r.SourceRange.Length >= value);
        if (range == null)
        {
            return value;
        }

        var startOffset = value - range.SourceRange.Start;
        var mappedValue = range.DestinationRange.Start + startOffset;
        
        return mappedValue;
    }
}
