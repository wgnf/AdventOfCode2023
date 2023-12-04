namespace AdventOfCode2023._4;

[Puzzle(4, "Day 4: Scratchcards")]
// ReSharper disable once UnusedType.Global
internal sealed class Puzzle4 : IPuzzle
{
    public string ExpectedExampleResultPart1 => "13";
    public string ExpectedExampleResultPart2 => "30";
    
    public string SolvePart1(IEnumerable<string> fileContents)
    {
        var cards = GetCardsFromFileContent(fileContents);
        var winningPointsAll = new List<int>();

        foreach (var card in cards)
        {
            var actualWinningNumbers = card.WinningNumbers.Intersect(card.ActualNumbers).ToList();
            if (actualWinningNumbers.Count == 0)
            {
                continue;
            }

            var winningPoints = 1;
            var timesToDouble = actualWinningNumbers.Count - 1;
            for (var i = 0; i < timesToDouble; i++)
            {
                winningPoints *= 2;
            }
            
            Console.WriteLine($"Card number [{card.Number}] has {winningPoints} points, because it matched {actualWinningNumbers.Count} times");
            
            winningPointsAll.Add(winningPoints);
        }
        
        return winningPointsAll.Sum().ToString();
    }

    public string SolvePart2(IEnumerable<string> fileContents)
    {
        var originalCards = GetCardsFromFileContent(fileContents);

        var countOfAllCards = new List<int>
        {
            originalCards.Count,
        };

        Parallel.ForEach(originalCards, card =>
        {
            var count = GetResultingCountOf(card, originalCards);
            countOfAllCards.Add(count);
        });

        return countOfAllCards.Sum().ToString();
    }

    private static List<Card> GetCardsFromFileContent(IEnumerable<string> fileContents)
    {
        var cards = new List<Card>();
        var cardNumber = 0;
        
        foreach (var line in fileContents)
        {
            var indexOfColon = line.IndexOf(':');
            var lineWithoutCardPart = line[(indexOfColon + 1)..];

            var lineSplit = lineWithoutCardPart.Split('|', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
            if (lineSplit.Length != 2)
            {
                throw new InvalidOperationException($"Line '{line}' has not the correct format");
            }

            var winningNumbers = lineSplit[0]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var actualNumbers = lineSplit[1]
                .Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            var card = new Card
            {
                Number = cardNumber + 1,
            };
            card.WinningNumbers.AddRange(winningNumbers);
            card.ActualNumbers.AddRange(actualNumbers);
            
            Console.WriteLine($"Card number [{card.Number}] has winning numbers: [{string.Join(", ", card.WinningNumbers)}] and actual numbers: [{string.Join(", ", card.ActualNumbers)}]");
            
            cards.Add(card);
            cardNumber++;
        }

        return cards;
    }

    private int GetResultingCountOf(Card card, IReadOnlyCollection<Card> originalCards)
    {
        var actualWinningNumbers = card.WinningNumbers.Intersect(card.ActualNumbers).ToList();
        var wonCards = originalCards
            .Where(possibleCardToCopy => possibleCardToCopy.Number > card.Number && possibleCardToCopy.Number <= card.Number + actualWinningNumbers.Count)
            .Select(cardToCopy => cardToCopy.Clone())
            .ToList();
        
        var countOfCard = wonCards.Count;

        foreach (var wonCard in wonCards)
        {
            countOfCard += GetResultingCountOf(wonCard, originalCards);
        }
        
        Console.WriteLine($"Determined card number [{card.Number}] to have a resulting count of {countOfCard}");

        return countOfCard;
    }
}
