namespace AdventOfCode2023._7;

internal sealed class Hand
{
    public string Cards { get; set; }

    public int BidAmount { get; set; }
    
    public HandType Type { get; set; }

    public uint Points { get; set; }
}
