namespace AdventOfCode2023._3;

[Puzzle(3, "Day 3: Gear Ratios")]
// ReSharper disable once UnusedType.Global
internal sealed class Puzzle3 : IPuzzle
{
    public string ExpectedExampleResultPart1 => "4361";
    public string ExpectedExampleResultPart2 => "467835";
    
    public string SolvePart1(IEnumerable<string> fileContents)
    {
        var (symbols, partNumbers) = Puzzle3Parser.GetSymbolsAndPartNumbers(fileContents);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(partNumbers, symbols);
        
        return partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Select(partNumber => partNumber.Value)
            .Sum()
            .ToString();
    }

    public string SolvePart2(IEnumerable<string> fileContents)
    {
        var (symbols, partNumbers) = Puzzle3Parser.GetSymbolsAndPartNumbers(fileContents);

        var gearSymbols = symbols.Where(symbol => symbol.Value == '*');
        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(partNumbers, gearSymbols);

        var gearRatios = new List<int>();
        
        foreach (var partNumberNextToSymbol in partNumbersNextToSymbols)
        {
            if (partNumberNextToSymbol.Value.Count != 2)
            {
                continue;
            }

            var gearRatio = partNumberNextToSymbol.Value.Aggregate(1, (ratio, partNumber) => ratio * partNumber.Value);
            gearRatios.Add(gearRatio);
        }
        
        return gearRatios.Sum().ToString();
    }
}
