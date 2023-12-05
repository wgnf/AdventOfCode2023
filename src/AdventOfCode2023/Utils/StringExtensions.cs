namespace AdventOfCode2023.Utils;

internal static class StringExtensions
{
    public static string[] SplitTrimRemoveEmpty(this string input, char splitChar)
    {
        return input.Split(splitChar, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
    }
}
