# Red Rover Code Puzzle Solution

## Building the Project

To build the project, navigate to the root directory where the `RedRoverCodePuzzle.sln` file is located and run the following command:

```bash
dotnet build
```

## Building the Project

To Run the Console App, navigate to the root directory where the `RedRoverCodePuzzle.sln` file is located, and run the following command:

```bash
dotnet run --project .\src\RedRoverChallenge.ConsoleApp\RedRoverChallenge.ConsoleApp.csproj
```

Then follow the instructions on the console.

## Run the test 

To run the tests navigate to the root directory where the `RedRoverCodePuzzle.sln` file is located, and run the following command:

```bash
dotnet test
```

# Requirements Analisys

- What should be validated? 
	- Max Depth
	- Length of the input
	- Special Characters
	- Empty Strings
	- Strings with spaces
	- Should invalid input halt execution?
- The example includes quotes; it's not clear if the quotes are part of the string. I am assuming they are not.
- The output will alphabetize all the children of the parent node. I am assuming they are all expected to be alphabetized.