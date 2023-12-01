namespace AdventOfCode2023;

[AttributeUsage(AttributeTargets.Class)]
internal sealed class PuzzleAttribute(int number, string title) : Attribute
{
    public int Number => number;
    public string Title => title;
}
