using System.Diagnostics;
using System.Reflection;
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
    Console.WriteLine($"\n\n=== PART {partNumber} ===");
    
    //EXAMPLE
    var exampleFile = $"Input/example{puzzleNumber}_{partNumber}.txt";
    if (!File.Exists(exampleFile))
    {
        Console.Error.WriteLine($"Could not find example file for part '{partNumber}': '{exampleFile}'");
        Environment.Exit(420);
    }

    var contentsExample = File.ReadAllLines(exampleFile);
    SolveExample(contentsExample, expectedExampleResult, solver);
    
    // INPUT
    var inputFile = $"Input/input{puzzleNumber}.txt";
    if (!File.Exists(inputFile))
    {
        Console.Error.WriteLine($"Could not find input file: '{inputFile}'");
        Environment.Exit(420);
    }
    
    var contentsInput = File.ReadAllLines(inputFile);
    SolveInput(contentsInput, solver);
}

void SolveExample(IEnumerable<string> fileContents, string expectedResult, Func<IEnumerable<string>, string> solver)
{
    Console.WriteLine("\n\n.. EXAMPLE ..\n\n");

    var stopWatch = Stopwatch.StartNew();
    var result = solver.Invoke(fileContents);
    stopWatch.Stop();
    
    Console.WriteLine("\n\n===================================================");
    Console.WriteLine($"Expected Result: {expectedResult}");
    Console.WriteLine($"Actual Result:   {result}");
    Console.WriteLine($"Took:            {stopWatch.ElapsedMilliseconds}ms");
    Console.WriteLine("===================================================");
    
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
    Console.WriteLine("\n\n.. INPUT ..\n\n");
    
    var stopWatch = Stopwatch.StartNew();
    var result = solver.Invoke(fileContents);
    stopWatch.Stop();
    
    Console.WriteLine("\n\n===================================================");
    Console.WriteLine($"Got following Result for INPUT: {result}");
    Console.WriteLine($"Took:                           {stopWatch.ElapsedMilliseconds}ms");
    Console.WriteLine("===================================================");
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
