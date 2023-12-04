namespace AdventOfCode2023._4;

[Puzzle(4, "Day 4: Scratchcards")]
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
            var actualWinningNumbers = card.ActualNumbers.Where(actualNumber => card.WinningNumbers.Contains(actualNumber)).ToList();
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
        var processedCards = new List<Card>(originalCards);
        bool listHasChanged;

        do
        {
            var (listChanged, newProcessedCards) = ProcessCards(processedCards, originalCards);

            listHasChanged = listChanged;
            processedCards = newProcessedCards;
            
            Console.WriteLine($"Processed card list currently has {processedCards.Count} items");
        } while (listHasChanged);

        return processedCards.Count.ToString();
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

    private static (bool, List<Card>) ProcessCards(IReadOnlyCollection<Card> processedCards, IReadOnlyCollection<Card> originalCards)
    {
        var listChanged = false;
        var newProcessedCards = new List<Card>(processedCards);
        
        foreach (var card in processedCards)
        {
            if (card.HasBeenProcessed)
            {
                continue;
            }
            
            var actualWinningNumbers = card.ActualNumbers.Where(actualNumber => card.WinningNumbers.Contains(actualNumber)).ToList();
            if (actualWinningNumbers.Count == 0)
            {
                card.HasBeenProcessed = true;
                continue;
            }
            
            Console.WriteLine($"Card number [{card.Number}] has {actualWinningNumbers.Count} matches");

            var cardsToCopy = originalCards
                .Where(possibleCardToCopy => possibleCardToCopy.Number > card.Number && possibleCardToCopy.Number <= card.Number + actualWinningNumbers.Count)
                .Select(cardToCopy => cardToCopy.Clone())
                .ToList();
            
            Console.WriteLine($"... thus copying card numbers: {string.Join(", ", cardsToCopy.Select(c => c.Number))}");
            
            newProcessedCards.AddRange(cardsToCopy);
            listChanged = true;
            card.HasBeenProcessed = true;
        }

        return (listChanged, newProcessedCards);
    }
}
