using System.Text;

namespace AdventOfCode2023._1;

[Puzzle(1, "Day 1: Trebuchet?!")]
// ReSharper disable once UnusedType.Global
public sealed class Puzzle1 : IPuzzle
{
    public string ExpectedExampleResultPart1 => "142";
    public string ExpectedExampleResultPart2 => "281";
    
    public string SolvePart1(IEnumerable<string> fileContents)
    {
        var numbers = new List<int>();
        
        foreach (var line in fileContents)
        {
            var numberBuilder = new StringBuilder();

            // forward search for first digit
            foreach (var @char in line)
            {
                if (char.IsDigit(@char))
                {
                    numberBuilder.Append(@char);
                    break;
                }
            }

            // backward search for last digit (line reversed)
            foreach (var @char in line.Reverse())
            {
                if (char.IsDigit(@char))
                {
                    numberBuilder.Append(@char);
                    break;
                }
            }

            var number = numberBuilder.ToString();
            if (number.Length != 2)
            {
                throw new InvalidOperationException($"Number '{number}' is wrong length?!");
            }

            if (!int.TryParse(number, out var intNumber))
            {
                throw new InvalidOperationException($"Number '{number}' cannot be converted into int");
            }
            
            numbers.Add(intNumber);
        }

        return numbers.Sum().ToString();
    }

    public string SolvePart2(IEnumerable<string> fileContents)
    {
        var values = new[]
        {
            new
            {
                Word = "1",
                Value = 1,
            },
            new
            {
                Word = "2",
                Value = 2,
            },
            new
            {
                Word = "3",
                Value = 3,
            },
            new
            {
                Word = "4",
                Value = 4,
            },
            new
            {
                Word = "5",
                Value = 5,
            },
            new
            {
                Word = "6",
                Value = 6,
            },
            new
            {
                Word = "7",
                Value = 7,
            },
            new
            {
                Word = "8",
                Value = 8,
            },
            new
            {
                Word = "9",
                Value = 9,
            },
            new
            {
                Word = "one",
                Value = 1,
            },
            new
            {
                Word = "two",
                Value = 2,
            },
            new
            {
                Word = "three",
                Value = 3,
            },
            new
            {
                Word = "four",
                Value = 4,
            },
            new
            {
                Word = "five",
                Value = 5,
            },
            new
            {
                Word = "six",
                Value = 6,
            },
            new
            {
                Word = "seven",
                Value = 7,
            },
            new
            {
                Word = "eight",
                Value = 8,
            },
            new
            {
                Word = "nine",
                Value = 9,
            },
        };

        var numbers = new List<int>();

        foreach (var line in fileContents)
        {
            var indexForwardSearch = line.Length;
            var valueForwardSearch = 0;

            var indexBackwardSearch = -1;
            var valueBackwardSearch = 0;
            
            foreach (var value in values)
            {
                var indexFirst = line.IndexOf(value.Word, StringComparison.InvariantCulture);
                var indexLast = line.LastIndexOf(value.Word, StringComparison.InvariantCulture);

                if (indexFirst < indexForwardSearch && indexFirst >= 0)
                {
                    indexForwardSearch = indexFirst;
                    valueForwardSearch = value.Value;
                }

                if (indexLast > indexBackwardSearch && indexFirst >= 0)
                {
                    indexBackwardSearch = indexLast;
                    valueBackwardSearch = value.Value;
                }
            }

            var numberValue = valueForwardSearch * 10 + valueBackwardSearch;
            numbers.Add(numberValue);
        }

        return numbers.Sum().ToString();
    }
}
