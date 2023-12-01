namespace AdventOfCode2023;

internal interface IPuzzle
{
    string ExpectedExampleResultPart1 { get; }
    string ExpectedExampleResultPart2 { get; }

    string SolvePart1(IEnumerable<string> fileContents);
    string SolvePart2(IEnumerable<string> fileContents);
}
