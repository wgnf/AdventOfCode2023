namespace AdventOfCode2023._5;

internal sealed class Almanac
{
    private readonly object _lockObject = new();
    
    public List<long> Seeds { get; } = [];

    public RangeMapSet SeedToSoilMap { get; } = new();

    public RangeMapSet SoilToFertilizerMap { get; } = new();

    public RangeMapSet FertilizerToWaterMap { get; } = new();

    public RangeMapSet WaterToLightMap { get; } = new();

    public RangeMapSet LightToTemperatureMap { get; } = new();

    public RangeMapSet TemperatureToHumidityMap { get; } = new();

    public RangeMapSet HumidityToLocationMap { get; } = new();

    public long GetClosestSeedLocation()
    {
        var closestLocation = long.MaxValue;

        Parallel.ForEach(Seeds, new ParallelOptions { MaxDegreeOfParallelism = 1 }, seed =>
        {
            Console.WriteLine($"Calculating location for seed {seed}...");
            
            var soil = SeedToSoilMap.GetMappedValue(seed);
            var fertilizer = SoilToFertilizerMap.GetMappedValue(soil);
            var water = FertilizerToWaterMap.GetMappedValue(fertilizer);
            var light = WaterToLightMap.GetMappedValue(water);
            var temperature = LightToTemperatureMap.GetMappedValue(light);
            var humidity = TemperatureToHumidityMap.GetMappedValue(temperature);
            var location = HumidityToLocationMap.GetMappedValue(humidity);

            lock (_lockObject)
            {
                if (location > closestLocation)
                {
                    return;
                }

                closestLocation = location;   
            }
        });

        return closestLocation;
    }
}
