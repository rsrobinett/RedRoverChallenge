using System.Text;

namespace RedRoverChallenge.SolveService.SolveHandler;

/// <summary>
/// 
/// </summary>
public class SimpleSolveHandler : ISolveHandler
{
    public string Solve(string input)
    {
        int depth = -1; // root is at depth -1
        StringBuilder stringBuilder = new StringBuilder();
        string LinePrefix() => $"{new string(' ', depth * 2)}- ";

        foreach (char c in input)
        {
            switch (c)
            {
                case '(':
                    depth++;
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.AppendLine();
                    }
                    if (depth < 0)
                    {
                        throw new ApplicationException("Invalid input, unbalanced parentheses");
                    }
                    stringBuilder.Append(LinePrefix());
                    break;
                case ')':
                    depth--;
                    if (depth < -1)
                    {
                        throw new ApplicationException("Invalid input, unbalanced parentheses");
                    }
                    break;
                case ',':
                    if (depth < 0)
                    {
                        throw new ApplicationException("Invalid input, unbalanced parentheses");
                    }
                    stringBuilder.AppendLine().Append(LinePrefix());
                    break;
                case ' ':
                    break;
                default:
                    stringBuilder.Append(c);
                    break;
            }
        }

        if (depth != -1)
        {
            throw new ApplicationException("Invalid input, unbalanced parentheses, did not return to level 0");
        }

        return stringBuilder.ToString();
    }
}