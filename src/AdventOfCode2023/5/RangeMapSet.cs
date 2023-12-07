namespace AdventOfCode2023._5;

public sealed class RangeMapSet
{
    public List<RangeMap> Items { get; } = [];

    public IEnumerable<Range> GetRanges(IEnumerable<Range> ranges)
    {
        var unMigratedRanges = new List<Range>(ranges.OrderBy(r => r.Start));
        var migratedRanges = new List<Range>();

        foreach (var rangeMap in Items)
        {
            ProcessFor(rangeMap, unMigratedRanges, migratedRanges);
        }

        return unMigratedRanges.Concat(migratedRanges);
    }

    private static void ProcessFor(RangeMap rangeMap, List<Range> unMigratedRanges, List<Range> migratedRanges)
    {
        // make copy so that adding in the loop is possible
        foreach (var unMigratedRange in unMigratedRanges.ToList())
        {
            if (!TryMigrateRangeFor(
                    rangeMap,
                    unMigratedRange,
                    out var newUnMigratedRanges,
                    out var resultingMigratedRanges))
            {
                continue;
            }

            unMigratedRanges.Remove(unMigratedRange);
                
            unMigratedRanges.AddRange(newUnMigratedRanges);
            migratedRanges.AddRange(resultingMigratedRanges);
        }
    }

    private static bool TryMigrateRangeFor(
        RangeMap rangeMap, 
        Range range,
        out List<Range> newUnMigratedRanges,
        out List<Range> migratedRanges)
    {
        newUnMigratedRanges = [];
        migratedRanges = [];

        // calculations...
        var givenRangeStart = range.Start;
        var givenRangeEnd = range.Start + range.Length;

        var sourceRange = rangeMap.SourceRange;
        var destinationRange = rangeMap.DestinationRange;

        var sourceRangeStart = sourceRange.Start;
        var sourceRangeEnd = sourceRange.Start + sourceRange.Length;

        var destinationRangeStart = destinationRange.Start;
        var destinationRangeEnd = destinationRange.Start + destinationRange.Length;

        // when the given range does not fit anywhere into the source-range, we cannot migrate it
        // this checks if the given range is partially part of the source-range
        var isInRangeMap = (sourceRangeStart >= givenRangeStart && sourceRangeStart <= givenRangeEnd) ||
                           (sourceRangeEnd >= givenRangeStart && sourceRangeEnd <= givenRangeEnd) ||
                           (givenRangeStart >= sourceRangeStart && givenRangeStart <= sourceRangeEnd) ||
                           (givenRangeEnd >= sourceRangeStart && givenRangeEnd <= sourceRangeEnd);
        if (!isInRangeMap)
        {
            return false;
        }

        var givenRangeStartOffset = givenRangeStart - sourceRangeStart;
        var givenRangeEndOffset = givenRangeEnd - sourceRangeEnd;

        // lets check if the given range fits completely into the target range
        if (givenRangeStartOffset == 0 && givenRangeEndOffset == 0)
        {
            migratedRanges.Add(rangeMap.DestinationRange);
            return true;
        }

        AddMigratedRangeWhenNoCompleteFit(
            migratedRanges, 
            givenRangeStartOffset, 
            destinationRangeStart, 
            givenRangeEndOffset, 
            destinationRangeEnd);

        ConsiderOverhangFront(newUnMigratedRanges, givenRangeStartOffset, givenRangeStart);
        ConsiderOverhangBack(newUnMigratedRanges, givenRangeEndOffset, sourceRangeEnd);

        return true;
    }

    private static void AddMigratedRangeWhenNoCompleteFit(
        List<Range> migratedRanges, 
        long givenRangeStartOffset, 
        long destinationRangeStart, 
        long givenRangeEndOffset, 
        long destinationRangeEnd)
    {
        long migratedStart;
        long migratedEnd;
        
        if (givenRangeStartOffset < 0)
        {
            migratedStart = destinationRangeStart;
        }
        else
        {
            migratedStart = destinationRangeStart + givenRangeStartOffset;
        }

        if (givenRangeEndOffset > 0)
        {
            migratedEnd = destinationRangeEnd;
        }
        else
        {
            migratedEnd = destinationRangeEnd + givenRangeEndOffset;
        }

        var migratedLength = migratedEnd - migratedStart;
        
        var migratedRange = new Range(migratedStart, migratedLength);
        migratedRanges.Add(migratedRange);
    }

    private static void ConsiderOverhangFront(
        List<Range> newUnMigratedRanges, 
        long givenRangeStartOffset, 
        long givenRangeStart)
    {
        // if the given-range starts in the source-range, there is no overhang
        if (givenRangeStartOffset >= 0)
        {
            return;
        }

        var overhangFrontStart = givenRangeStart;
        var overhangFrontEnd = givenRangeStart - givenRangeStartOffset - 1;
        var overhandFrontLength = overhangFrontEnd - overhangFrontStart;
            
        newUnMigratedRanges.Add(new Range(overhangFrontStart, overhandFrontLength));
    }

    private static void ConsiderOverhangBack(
        List<Range> newUnMigratedRanges, 
        long givenRangeEndOffset,
        long sourceRangeEnd)
    {
        // if the given-range ends in the source-range, there is no overhang
        if (givenRangeEndOffset <= 0)
        {
            return;
        }

        var overhangBackStart = sourceRangeEnd + 1;
        // offset should be negative in all cases
        var overhangBackEnd = sourceRangeEnd + givenRangeEndOffset;
        var overhangBackLength = overhangBackEnd - overhangBackStart;
            
        newUnMigratedRanges.Add(new Range(overhangBackStart, overhangBackLength));
    }
}
