using AdventOfCode2023._5;
using FluentAssertions;
using Xunit;
using Range = AdventOfCode2023._5.Range;

namespace AdventOfCodeTests;

public sealed class Puzzle5Tests
{
    [Fact]
    public void RangeMap_Handles_Overhang_Front()
    {
        var inputRange = new Range(100, 100);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();

        // 100 - 149 -> overhang front
        result
            .Should()
            .Contain(range => range.Start == 100 &&
                              range.Length == 49);
        
        // 200 - 250 -> migrated to destination
        result
            .Should()
            .Contain(range => range.Start == 200 &&
                              range.Length == 50);
    }
    
    [Fact]
    public void RangeMap_Handles_Overhang_Back()
    {
        var inputRange = new Range(150, 100);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();

        // 201 - 250 -> overhang back
        result
            .Should()
            .Contain(range => range.Start == 201 &&
                              range.Length == 49);
        
        // 200 - 250 -> migrated to destination
        result
            .Should()
            .Contain(range => range.Start == 200 &&
                              range.Length == 50);
    }
    
    [Fact]
    public void RangeMap_Handles_Overhang_Front_And_Back()
    {
        var inputRange = new Range(100, 150);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();
        
        // 100 - 149 -> overhang front
        result
            .Should()
            .Contain(range => range.Start == 100 &&
                              range.Length == 49);

        // 201 - 250 -> overhang back
        result
            .Should()
            .Contain(range => range.Start == 201 &&
                              range.Length == 49);
        
        // 200 - 250 -> migrated to destination
        result
            .Should()
            .Contain(range => range.Start == 200 &&
                              range.Length == 50);
    }
    
    [Fact]
    public void RangeMap_Handles_Not_In_Range()
    {
        var inputRange = new Range(250, 100);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();
        
        // 250 - 350 -> not migrated because not in range
        result
            .Should()
            .OnlyContain(range => range.Start == 250 &&
                              range.Length == 100);
    }
    
    [Fact]
    public void RangeMap_Handles_Range_Completely_In_Source()
    {
        var inputRange = new Range(150, 50);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();
        
        // 200 - 250 -> only the migrated one, because it completely fits
        result
            .Should()
            .OnlyContain(range => range.Start == 200 &&
                              range.Length == 50);
    }
    
    [Fact]
    public void RangeMap_Handles_Range_Partly_In_Source()
    {
        var inputRange = new Range(175, 10);

        var rangeMapSource = new Range(150, 50);
        var rangeMapDestination = new Range(200, 50);
        var rangeMap = new RangeMap(rangeMapSource, rangeMapDestination);
        var rangeMapSet = new RangeMapSet
        {
            Items = { rangeMap },
        };

        var result = rangeMapSet
            .GetRanges(new[] { inputRange })
            .ToList();
        
        // 225 - 235 -> only the migrated one, because it partly fits
        result
            .Should()
            .OnlyContain(range => range.Start == 225 &&
                              range.Length == 10);
    }
}
