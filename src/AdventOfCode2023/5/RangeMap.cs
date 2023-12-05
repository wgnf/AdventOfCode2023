using System.Text;

namespace AdventOfCode2023._5;

internal sealed class RangeMap
{
    public RangeMap(Range sourceRange, Range destinationRange)
    {
        SourceRange = sourceRange;
        DestinationRange = destinationRange;
    }

    public Range SourceRange { get; }

    public Range DestinationRange { get; }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append($"SOURCE = {SourceRange} ; ");
        builder.Append($"DESTINATION = {DestinationRange}");

        return builder.ToString();
    }
}
