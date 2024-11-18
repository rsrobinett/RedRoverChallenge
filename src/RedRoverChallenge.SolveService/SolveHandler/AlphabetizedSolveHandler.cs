using System.Text;

namespace RedRoverChallenge.SolveService.SolveHandler;

public class AlphabetizedSolveHandler : ISolveHandler
{
    public string Solve(string input)
    {
        List<string> tokens = TokenizeInput(input);

        IEnumerable<HierarchicalElement> elements = CreateElementList(tokens);

        string output = BuildOutput(elements);

        return output;
    }

    private static List<HierarchicalElement> CreateElementList(List<string> tokens)
    {
        Stack<HierarchicalElement> parentStack = new();
        HierarchicalElement currentElement = new()
        {
            Value = "root"
        };

        foreach (string token in tokens)
        {
            switch (token)
            {
                case "(":
                    // create a new parent element from the current element
                    currentElement.Children = new List<HierarchicalElement>();
                    parentStack.Push(currentElement);
                    break;
                case ")":
                    // pop the parent element from the stack and sort its children
                    HierarchicalElement parent = parentStack.Pop();
                    parent.Children?.Sort((e1, e2) => string.Compare(e1.Value, e2.Value, StringComparison.Ordinal));
                    if (parentStack.Count == 0)
                    {
                        return parent.Children ?? new List<HierarchicalElement>();
                    }
                    break;
                case ",":
                    // separator, do nothing
                    break;
                default:
                    // create a new element and add it to the parent's children
                    currentElement = new HierarchicalElement { Value = token };
                    parentStack.Peek().Children?.Add(currentElement);
                    break;
            }
        }

        if (parentStack.Count > 1)
        {
            throw new ApplicationException("Invalid input, unbalanced parentheses, did not return to level 0");
        }

        return parentStack.Pop().Children ?? new List<HierarchicalElement>();
    }

    private static List<string> TokenizeInput(string input)
    {
        var tokens = new List<string>();
        var tokenBuilder = new StringBuilder();

        foreach (char c in input)
        {
            if (char.IsWhiteSpace(c))
            {
                continue;
            }

            // much faster than using regex
            if (c == '(' || c == ')' || c == ',')
            {
                if (tokenBuilder.Length > 0)
                {
                    tokens.Add(tokenBuilder.ToString());
                    tokenBuilder.Clear();
                }
                tokens.Add(c.ToString());
            }
            else
            {
                tokenBuilder.Append(c);
            }
        }

        if (tokenBuilder.Length > 0)
        {
            tokens.Add(tokenBuilder.ToString());
        }

        return tokens;
    }

    private string BuildOutput(IEnumerable<HierarchicalElement> elements)
    {
        StringBuilder output = new();
        BuildOutputRecursive(elements, output, 0);
        return output.ToString();
    }

    /// <summary>
    /// Recursively builds the output string from the hierarchical elements. This method assumes it is already in the correct order. 
    /// </summary>
    /// <param name="elements">The elements to process.</param>
    /// <param name="output">The StringBuilder to append output to.</param>
    /// <param name="depth">The current depth in the hierarchy.</param>
    private void BuildOutputRecursive(IEnumerable<HierarchicalElement> elements, StringBuilder output, int depth)
    {
        foreach (HierarchicalElement element in elements)
        {
            if (output.Length > 0)
            {
                output.AppendLine();
            }

            output.Append(new string(' ', depth * 2)).Append("- ").Append(element.Value);

            if (element.Children?.Any() ?? false)
            {
                BuildOutputRecursive(element.Children, output, depth + 1);
            }
        }
    }

    private class HierarchicalElement
    {
        public string? Value { get; set; }
        public List<HierarchicalElement>? Children { get; set; }
    }
}