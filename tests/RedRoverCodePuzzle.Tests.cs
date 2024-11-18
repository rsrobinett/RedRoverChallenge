using System.Diagnostics;
using RedRoverChallenge.SolveService;
using RedRoverChallenge.SolveService.SolveHandler;
using Xunit.Abstractions;

namespace RedRoverChallengeTests;

public class RedRoverCodePuzzleTests(ITestOutputHelper output)
{
    private readonly RedRoverCodePuzzle _redRoverCodePuzzle = new();
    private readonly ISolveHandler[] _solveHandlers = { new SimpleSolveHandler(), new AlphabetizedSolveHandler() };

    [Theory]
    [InlineData("(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)")]
    public void Output(string input)
    {

        output.WriteLine("***** Begin Result *****");
        output.WriteLine($"Input: {input}");
        output.WriteLine("");

        output.WriteLine($"Output of {nameof(SimpleSolveHandler)}");
        output.WriteLine(_redRoverCodePuzzle.Solve(input, new SimpleSolveHandler()));
        
        output.WriteLine("");
        output.WriteLine($"Output of {nameof(AlphabetizedSolveHandler)}");
        output.WriteLine(_redRoverCodePuzzle.Solve(input, new AlphabetizedSolveHandler()));

        output.WriteLine("***** End Result *****");
    }

    [Fact]
    public void Solve_Simple()
    {
        // arrange
        string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        string expectedOutput =
            "- id\r\n- name\r\n- email\r\n- type\r\n  - id\r\n  - name\r\n  - customFields\r\n    - c1\r\n    - c2\r\n    - c3\r\n- externalId";

        // act
        string output = _redRoverCodePuzzle.Solve(input, new SimpleSolveHandler());


        // assert
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData("(id, name, email, type(id, name, customFields(c1, c2, c3), externalId)")] // Missing closing parenthesis
    [InlineData("id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)")] // Missing opening parenthesis
    public void Solve_InvalidParentheses_ShouldThrowException(string input)
    {
        // Arrange
        var solveHandler = new SimpleSolveHandler();

        // Act & Assert
        Assert.Throws<ApplicationException>(() => _redRoverCodePuzzle.Solve(input, solveHandler));
    }

    [Theory]
    [InlineData("(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)")]
    [InlineData("(name, email, type(id, name, customFields(c3, c2, c1)), externalId, id)")]
    [InlineData("(type(id, name, customFields(c3, c2, c1)), externalId, id,name, email,)")]
    public void Solve_Alphabetized(string input)
    {
        // arrange
        string expectedOutput =
            "- email\r\n- externalId\r\n- id\r\n- name\r\n- type\r\n  - customFields\r\n    - c1\r\n    - c2\r\n    - c3\r\n  - id\r\n  - name";

        // act
        string output = _redRoverCodePuzzle.Solve(input, new AlphabetizedSolveHandler());

        // assert
        Assert.Equal(expectedOutput, output);
    }

    [Theory]
    [InlineData(10)]
    [InlineData(100)]
    [InlineData(1000)]
    [InlineData(1000000)]
    public void Solve_AllHandlers_PerformanceTest(int iterations)
    {
        // arrange
        string input = "(id, name, email, type(id, name, customFields(c1, c2, c3)), externalId)";
        Stopwatch stopwatch = new Stopwatch();

        for (int j = 0; j < 3; j++)
        {
            output.WriteLine($"Iteration: {j}");
            foreach (var solveHandler in _solveHandlers)
            {
                // act
                stopwatch.Reset();
                stopwatch.Start();
                for (int i = 0; i < iterations; i++)
                {
                    _redRoverCodePuzzle.Solve(input, solveHandler);
                }

                stopwatch.Stop();

                // assert
                output.WriteLine(
                    $"Handler: {solveHandler.GetType().Name}, Time taken for {iterations} iterations: {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}

