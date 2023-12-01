﻿using System.Reflection;
using AdventOfCode2023;

Console.WriteLine("...::: ADVENT OF CODE 2023 :::...\n");

var puzzles = GetPuzzles().ToList();

Console.WriteLine("Found following puzzles, which one do you want to solve?");

foreach (var puzzle in puzzles)
{
    Console.WriteLine($"[{puzzle.Number}]: \"{puzzle.Title}\"");   
}

var input = Console.ReadLine();
if (!int.TryParse(input, out var puzzleNumberToSolve))
{
    Console.WriteLine("OOPS sorry I didn't understand that!");
    return;
}

var puzzleToSolve = puzzles.First(p => p.Number == puzzleNumberToSolve);
SolvePuzzle(puzzleNumberToSolve, puzzleToSolve.Instance);

return;

void SolvePuzzle(int puzzleNumber, IPuzzle puzzle)
{
    SolvePart(1, puzzleNumber, puzzle.ExpectedExampleResultPart1, puzzle.SolvePart1);
    SolvePart(2, puzzleNumber, puzzle.ExpectedExampleResultPart2, puzzle.SolvePart2);
}

void SolvePart(int partNumber, int puzzleNumber, string expectedExampleResult, Func<IEnumerable<string>, string> solver)
{
    Console.WriteLine($"=== PART {partNumber} ===");
    
    //EXAMPLE
    var exampleFile = $"Input/example{puzzleNumber}_{partNumber}.txt";
    if (!File.Exists(exampleFile))
    {
        Console.Error.WriteLine($"Could not find example file for part {partNumber}");
        Environment.Exit(420);
    }

    var contentsExample = File.ReadAllLines(exampleFile);
    SolveExample(contentsExample, expectedExampleResult, solver);
    
    // INPUT
    var inputFile = $"Input/input{puzzleNumber}_{partNumber}.txt";
    if (!File.Exists(inputFile))
    {
        Console.Error.WriteLine($"Could not find input file for part {partNumber}");
        Environment.Exit(420);
    }
    
    var contentsInput = File.ReadAllLines(inputFile);
    SolveInput(contentsInput, solver);
}

void SolveExample(IEnumerable<string> fileContents, string expectedResult, Func<IEnumerable<string>, string> solver)
{
    Console.WriteLine(".. EXAMPLE ..");

    var result = solver.Invoke(fileContents);
    
    Console.WriteLine($"Expected Result: {expectedResult}");
    Console.WriteLine($"Actual Result:   {result}");
    
    if (expectedResult.Equals(result))
    {
        Console.WriteLine("CORRECT");
    }
    else
    {
        Console.Error.WriteLine("WRONG");
        Environment.Exit(69);
    }
}

void SolveInput(IEnumerable<string> fileContents, Func<IEnumerable<string>, string> solver)
{
    Console.WriteLine(".. INPUT ..");
    
    var result = solver.Invoke(fileContents);
    
    Console.WriteLine($"✨ Got following Result: {result} ✨");
}

IEnumerable<PuzzleDto> GetPuzzles()
{
    var assembly = Assembly.GetExecutingAssembly();
    var puzzleDtos = new List<PuzzleDto>();

    foreach (var type in assembly.DefinedTypes)
    {
        if (!type.ImplementedInterfaces.Contains(typeof(IPuzzle)))
        {
            continue;
        }

        var puzzleAttribute = type.GetCustomAttribute<PuzzleAttribute>();
        if (puzzleAttribute == null)
        {
            continue;
        }

        var puzzleInstance = (IPuzzle?) Activator.CreateInstance(type);
        if (puzzleInstance == null)
        {
            continue;
        }

        var puzzleDto = new PuzzleDto(puzzleAttribute.Number, puzzleAttribute.Title, puzzleInstance);
        puzzleDtos.Add(puzzleDto);
    }

    return puzzleDtos;
}

record struct PuzzleDto(int Number, string Title, IPuzzle Instance);
