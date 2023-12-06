namespace AdventOfCode2023._4;

internal sealed class Card
{
    public int Number { get; set; }
    
    public List<int> WinningNumbers { get; private init; } = [];

    public List<int> ActualNumbers { get; private init; } = [];
}
