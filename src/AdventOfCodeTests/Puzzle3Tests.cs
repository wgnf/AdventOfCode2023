using AdventOfCode2023._3;
using FluentAssertions;
using Xunit;

namespace AdventOfCodeTests;

public sealed class Puzzle3Tests
{
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Top_Left()
    {
        var symbol = new Symbol('*', 3, 5);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Top()
    {
        var symbol = new Symbol('*', 3, 6);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Top_Right()
    {
        var symbol = new Symbol('*', 3, 7);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Right()
    {
        var symbol = new Symbol('*', 4, 7);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Bottom_Right()
    {
        var symbol = new Symbol('*', 5, 7);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Bottom()
    {
        var symbol = new Symbol('*', 5, 6);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Bottom_Left()
    {
        var symbol = new Symbol('*', 5, 5);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v)
            .Should()
            .ContainSingle(p => p == partNumber);
    }
    
    [Fact]
    public void Should_Find_Single_Digit_PartNumber_Next_To_Symbol_Left()
    {
        var symbol = new Symbol('*', 4, 5);
        var partNumber = new PartNumber(123, 4, 6, 6);

        var partNumbersNextToSymbols = Puzzle3NeighborFinder.GetPartNumbersNextToSymbols(new[] { partNumber }, new[] { symbol });

        partNumbersNextToSymbols
            .Values
            .SelectMany(v => v) 
            .Should()
            .ContainSingle(p => p == partNumber);
    }

    [Fact]
    public void Should_Parse_PartNumber_At_Line_End_Correctly()
    {
        var fileContents = new List<string>
        {
            "...4",
        };

        var (symbols, partNumbers) = Puzzle3Parser.GetSymbolsAndPartNumbers(fileContents);

        symbols
            .Should()
            .BeEmpty();

        partNumbers
            .Should()
            .ContainSingle(p => p.Value == 4 &&
                                p.LineNumber == 0 &&
                                p.StartIndex == 3 &&
                                p.EndIndex == 3);
    }
}
