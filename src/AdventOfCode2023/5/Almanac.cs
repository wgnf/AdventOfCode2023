using System.Collections.Concurrent;
using System.Text;

namespace AdventOfCode2023._5;

internal sealed class Almanac
{
    public List<long> Seeds { get; } = [];

    public RangeMapSet SeedToSoilMap { get; } = new();

    public RangeMapSet SoilToFertilizerMap { get; } = new();

    public RangeMapSet FertilizerToWaterMap { get; } = new();

    public RangeMapSet WaterToLightMap { get; } = new();

    public RangeMapSet LightToTemperatureMap { get; } = new();

    public RangeMapSet TemperatureToHumidityMap { get; } = new();

    public RangeMapSet HumidityToLocationMap { get; } = new();

    private readonly ConcurrentDictionary<long, long> _seedToLocationMap = new();

    public IEnumerable<long> GetSeedLocations()
    {
        var locations = new ConcurrentBag<long>();

        Parallel.ForEach(Seeds, new ParallelOptions { MaxDegreeOfParallelism = -1 }, seed =>
        {
            if (_seedToLocationMap.TryGetValue(seed, out var location))
            {
                Console.WriteLine("CACHE HIT");
                locations.Add(location);
                return;
            }
            
            var soil = SeedToSoilMap.GetMappedValue(seed);
            var fertilizer = SoilToFertilizerMap.GetMappedValue(soil);
            var water = FertilizerToWaterMap.GetMappedValue(fertilizer);
            var light = WaterToLightMap.GetMappedValue(water);
            var temperature = LightToTemperatureMap.GetMappedValue(light);
            var humidity = TemperatureToHumidityMap.GetMappedValue(temperature);
            location = HumidityToLocationMap.GetMappedValue(humidity);

            _seedToLocationMap.TryAdd(seed, location);
            locations.Add(location);
        });

        return locations;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"SEEDS = {string.Join(", ", Seeds)}");
        builder.AppendLine($"SEED-TO-SOIL = {CollectionToString(SeedToSoilMap.Items)}");
        builder.AppendLine($"SOIL-TO-FERTILIZER = {CollectionToString(SoilToFertilizerMap.Items)}");
        builder.AppendLine($"FERTILIZER-TO-WATER = {CollectionToString(FertilizerToWaterMap.Items)}");
        builder.AppendLine($"WATER-TO-LIGHT = {CollectionToString(WaterToLightMap.Items)}");
        builder.AppendLine($"LIGHT-TO-TEMPERATURE = {CollectionToString(LightToTemperatureMap.Items)}");
        builder.AppendLine($"TEMPERATURE-TO-HUMIDITY = {CollectionToString(TemperatureToHumidityMap.Items)}");
        builder.AppendLine($"HUMIDITY-TO-LOCATION = {CollectionToString(HumidityToLocationMap.Items)}");

        return builder.ToString();
    }

    private static string CollectionToString(IEnumerable<RangeMap> rangeMaps)
    {
        var builder = new StringBuilder();
        builder.AppendLine();

        builder.AppendJoin("\n", rangeMaps.Select(rangeMap => $"\t - {rangeMap}"));

        return builder.ToString();
    }
}
