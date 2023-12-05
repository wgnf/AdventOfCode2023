using System.Text;

namespace AdventOfCode2023._5;

internal sealed class Almanac
{
    public List<long> Seeds { get; } = [];

    public List<RangeMap> SeedToSoilMap { get; } = [];

    public List<RangeMap> SoilToFertilizerMap { get; } = [];

    public List<RangeMap> FertilizerToWaterMap { get; } = [];

    public List<RangeMap> WaterToLightMap { get; } = [];

    public List<RangeMap> LightToTemperatureMap { get; } = [];

    public List<RangeMap> TemperatureToHumidityMap { get; } = [];

    public List<RangeMap> HumidityToLocationMap { get; } = [];

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.AppendLine($"SEEDS = {string.Join(", ", Seeds)}");
        builder.AppendLine($"SEED-TO-SOIL = {CollectionToString(SeedToSoilMap)}");
        builder.AppendLine($"SOIL-TO-FERTILIZER = {CollectionToString(SoilToFertilizerMap)}");
        builder.AppendLine($"FERTILIZER-TO-WATER = {CollectionToString(FertilizerToWaterMap)}");
        builder.AppendLine($"WATER-TO-LIGHT = {CollectionToString(WaterToLightMap)}");
        builder.AppendLine($"LIGHT-TO-TEMPERATURE = {CollectionToString(LightToTemperatureMap)}");
        builder.AppendLine($"TEMPERATURE-TO-HUMIDITY = {CollectionToString(TemperatureToHumidityMap)}");
        builder.AppendLine($"HUMIDITY-TO-LOCATION = {CollectionToString(HumidityToLocationMap)}");

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
