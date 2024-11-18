using RedRoverChallenge.SolveService.SolveHandler;

namespace RedRoverChallenge.SolveService;

/// <summary>
/// Solves the Red Rover code puzzle using the specified solve handler.
/// </summary>
/// <param name="input">The input string to process.</param>
/// <param name="solveHandler">The solve handler that defines the processing logic.</param>
/// <returns>The processed output string.</returns>
public class RedRoverCodePuzzle
{
    public string Solve(string input, ISolveHandler solveHandler)
    {
        return solveHandler.Solve(input);
    }
}