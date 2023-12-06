namespace AdventOfCode2023._6;

internal sealed class Race
{
    public long MaxTime { get; set; }

    public long RecordDistance { get; set; }

    public IEnumerable<long> GetButtonHoldsToWin()
    {
        var waysToWin = new List<long>();

        for (var buttonHoldTime = 0; buttonHoldTime < MaxTime; buttonHoldTime++)
        {
            var speed = buttonHoldTime;
            var remainingTime = MaxTime - buttonHoldTime;
            var distance = remainingTime * speed;

            if (distance > RecordDistance)
            {
                waysToWin.Add(buttonHoldTime);
            }
            // when we already found ways to win, but did not find a new way to win, we can be sure that we won't find any more ways to win
            else if (waysToWin.Count != 0)
            {
                break;
            }
        }

        return waysToWin;
    }
}
