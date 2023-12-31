﻿namespace AdventOfCode2023._5;

public sealed class RangeMap
{
    public RangeMap(Range sourceRange, Range destinationRange)
    {
        SourceRange = sourceRange;
        DestinationRange = destinationRange;
    }

    public Range SourceRange { get; }

    public Range DestinationRange { get; }
}
