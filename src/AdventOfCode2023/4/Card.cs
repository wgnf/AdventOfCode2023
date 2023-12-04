namespace AdventOfCode2023._4;

internal sealed class Card
{
    public int Number { get; set; }
    
    public List<int> WinningNumbers { get; private init; } = [];

    public List<int> ActualNumbers { get; private init; } = [];

    public bool HasBeenProcessed { get; set; }

    public Card Clone()
    {
        return new Card
        {
            Number = Number,
            WinningNumbers = WinningNumbers,
            ActualNumbers = ActualNumbers ,
            HasBeenProcessed = false,
        };
    }
}
