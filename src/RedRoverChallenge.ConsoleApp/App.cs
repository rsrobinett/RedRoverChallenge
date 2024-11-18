using RedRoverChallenge.SolveService;
using RedRoverChallenge.SolveService.SolveHandler;

namespace RedRoverChallenge.ConsoleApp;

public class App(IEnumerable<ISolveHandler> solvers)
{
    public void Run()
    {
        while (true)
        {
            // Request input string
            string? input = null;
            while (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Enter the input string:");
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            }

            // Print numbered options for solvers
            Console.WriteLine("Choose a solver:");
            var solversList = solvers.ToList();
            for (int i = 0; i < solversList.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {solversList[i].GetType().Name}");
            }

            // Request solver choice
            string? inputChoice = Console.ReadLine();
            int choice;
            while (!int.TryParse(inputChoice, out choice) || choice < 1 || choice > solversList.Count)
            {
                Console.WriteLine("Invalid choice. Please enter a valid number corresponding to a solver:");
                inputChoice = Console.ReadLine();
            }

            // Call RedRoverCodePuzzle.Solve with the chosen solver
            RedRoverCodePuzzle puzzle = new RedRoverCodePuzzle();
            string result = puzzle.Solve(input, solversList[choice - 1]);

            // Print the result
            Console.WriteLine("Processed output:");
            Console.WriteLine(result);

            // Ask if the user wants to run again
            Console.WriteLine("Do you want to run again? (y/n)");
            string? runAgain = Console.ReadLine();
            if (runAgain?.ToLower() != "y")
            {
                break;
            }
        }
    }
}
