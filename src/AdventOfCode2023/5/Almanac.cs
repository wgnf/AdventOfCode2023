using System.Collections.Concurrent;

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

        Parallel.ForEach(Seeds, new ParallelOptions { MaxDegreeOfParallelism = 1 }, seed =>
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
}
