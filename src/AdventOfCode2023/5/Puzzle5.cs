using AdventOfCode2023.Utils;

namespace AdventOfCode2023._5;

[Puzzle(5, "Day 5: If You Give A Seed A Fertilizer")]
// ReSharper disable once UnusedType.Global
internal sealed class Puzzle5 : IPuzzle
{
    public string ExpectedExampleResultPart1 => "35";
    public string ExpectedExampleResultPart2 => "46";
    
    public string SolvePart1(IEnumerable<string> fileContents)
    {
        var almanac = ParseAlmanac(fileContents, false);
        var closestSeedLocation = almanac.GetClosestSeedLocation();

        return closestSeedLocation.ToString();
    }

    public string SolvePart2(IEnumerable<string> fileContents)
    {
        var almanac = ParseAlmanac(fileContents, true);
        var closestSeedLocation = almanac.GetClosestSeedLocation();

        return closestSeedLocation.ToString();
    }

    private static Almanac ParseAlmanac(IEnumerable<string> fileContents, bool considerSeedsAsRanged)
    {
        var firstLine = true;
        var almanac = new Almanac();
        List<RangeMap>? currentDestinationList = null;
        
        foreach (var line in fileContents)
        {
            if (line == string.Empty)
            {
                continue;
            }
            
            if (firstLine)
            {
                ParsSeeds(line, almanac, considerSeedsAsRanged);
                firstLine = false;
                continue;
            }

            if (line.Contains('-') && line.EndsWith(':'))
            {
                currentDestinationList = DetermineDestinationList(line, almanac);
                continue;
            }

            ParseMaps(line, currentDestinationList!);
        }

        return almanac;
    }

    private static void ParsSeeds(string line, Almanac almanac, bool considerSeedsAsRanges)
    {
        var lineSplit = line.SplitTrimRemoveEmpty(':');
        if (lineSplit.Length != 2)
        {
            throw new InvalidOperationException($"First '{line}' line has not expected format");
        }

        var seedTexts = lineSplit[1].SplitTrimRemoveEmpty(' ');
        
        if (!considerSeedsAsRanges)
        {
            almanac.Seeds.AddRange(seedTexts.Select(long.Parse));   
        }
        else
        {
            for (var index = 0; index < seedTexts.Length; index += 2)
            {
                var rangeStart = long.Parse(seedTexts[index]);
                var rangeLength = long.Parse(seedTexts[index + 1]);

                var seeds = EnumerableUtils.LongRange(rangeStart, rangeLength);
                almanac.Seeds.AddRange(seeds);
            }
        }
    }

    private static List<RangeMap> DetermineDestinationList(string line, Almanac almanac)
    {
        return line switch
        {
            "seed-to-soil map:" => almanac.SeedToSoilMap.Items,
            "soil-to-fertilizer map:" => almanac.SoilToFertilizerMap.Items,
            "fertilizer-to-water map:" => almanac.FertilizerToWaterMap.Items,
            "water-to-light map:" => almanac.WaterToLightMap.Items,
            "light-to-temperature map:" => almanac.LightToTemperatureMap.Items,
            "temperature-to-humidity map:" => almanac.TemperatureToHumidityMap.Items,
            "humidity-to-location map:" => almanac.HumidityToLocationMap.Items,
            _ => throw new ArgumentOutOfRangeException(nameof(line), line, "Oops unrecognized category"),
        };
    }
    
    // ReSharper disable once SuggestBaseTypeForParameter
    private static void ParseMaps(string line, List<RangeMap> currentDestinationList)
    {

        var lineSplit = line.SplitTrimRemoveEmpty(' ');
        if (lineSplit.Length != 3)
        {
            throw new InvalidOperationException($"Line '{line}' is not of expected format");
        }

        var destinationStart = long.Parse(lineSplit[0]);
        var sourceStart = long.Parse(lineSplit[1]);
        var rangeLength = long.Parse(lineSplit[2]);

        var destinationRange = new Range(destinationStart, rangeLength);
        var sourceRange = new Range(sourceStart, rangeLength);

        currentDestinationList.Add(new RangeMap(sourceRange, destinationRange));
    }
}
