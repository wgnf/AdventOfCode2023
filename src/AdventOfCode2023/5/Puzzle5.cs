using AdventOfCode2023.Utils;

namespace AdventOfCode2023._5;

[Puzzle(5, "Day 5: If You Give A Seed A Fertilizer")]
internal sealed class Puzzle5 : IPuzzle
{
    public string ExpectedExampleResultPart1 => "35";
    public string ExpectedExampleResultPart2 => "?";
    
    public string SolvePart1(IEnumerable<string> fileContents)
    {
        var almanac = ParseAlmanac(fileContents);
        var nearestLocation = long.MaxValue;

        foreach (var seed in almanac.Seeds)
        {
            var seedLocation = GetLocation(seed, almanac);
            if (seedLocation < nearestLocation)
            {
                nearestLocation = seedLocation;
            }
        }

        return nearestLocation.ToString();
    }

    public string SolvePart2(IEnumerable<string> fileContents)
    {
        return string.Empty;
    }

    private static Almanac ParseAlmanac(IEnumerable<string> fileContents)
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
                ParsSeeds(line, almanac);
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

        Console.WriteLine($"ALMANAC:\n{almanac}");
        return almanac;
    }

    private static void ParsSeeds(string line, Almanac almanac)
    {
        var lineSplit = line.SplitTrimRemoveEmpty(':');
        if (lineSplit.Length != 2)
        {
            throw new InvalidOperationException($"First '{line}' line has not expected format");
        }

        var seedTexts = lineSplit[1].SplitTrimRemoveEmpty(' ');
        almanac.Seeds.AddRange(seedTexts.Select(long.Parse));
    }

    private static List<RangeMap> DetermineDestinationList(string line, Almanac almanac)
    {
        return line switch
        {
            "seed-to-soil map:" => almanac.SeedToSoilMap,
            "soil-to-fertilizer map:" => almanac.SoilToFertilizerMap,
            "fertilizer-to-water map:" => almanac.FertilizerToWaterMap,
            "water-to-light map:" => almanac.WaterToLightMap,
            "light-to-temperature map:" => almanac.LightToTemperatureMap,
            "temperature-to-humidity map:" => almanac.TemperatureToHumidityMap,
            "humidity-to-location map:" => almanac.HumidityToLocationMap,
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

    private static int GetLocation(long seed, Almanac almanac)
    {
        return 0;
    }
}
