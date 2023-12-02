using System.Text;

namespace AdventOfCode2023._2;

internal sealed class Game
{
    public int Id { get; set; }

    public List<CubeRound> CubeRounds { get; } = new();

    public override string ToString()
    {
        var builder = new StringBuilder();

        builder.Append('<');
        builder.Append($" Id = {Id} ");

        foreach (var cubeRound in CubeRounds)
        {
            builder.Append('{');

            foreach (var revealedCube in cubeRound.RevealedCubes)
            {
                builder.Append('(');

                builder.Append($" Color = {revealedCube.Color}; Amount = {revealedCube.Amount} ");
                
                builder.Append(')');
            }
            
            builder.Append('}');
        }
        
        builder.Append('>');

        return builder.ToString();
    }
}
